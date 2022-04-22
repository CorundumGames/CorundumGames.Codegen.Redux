# `CorundumGames.Codegen.DisposableComponent`

Generates systems that call `Dispose()` on components that implement [`IDisposable`](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netstandard-2.1).
No attributes necessary, simply implement `IDisposable`.

This is useful for components that contain data that *must* be cleaned up, including pooled objects.
In my case, I use this plugin to reset [DOTween](http://dotween.demigiant.com)-based tweens.
Here's an example of a disposable component that I'm using in [Chromavaders](https://corundum.games):

```csharp
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JCMG.EntitasRedux;
using UnityEngine;

// This component is hand-written
[Game]
public sealed class PositionTweenComponent : IComponent, IDisposable
{
    public TweenerCore<Vector2, Vector2, VectorOptions> tween;

    public void Dispose()
    {
        if (tween is { active: true })
        { // If this position tween is not null and it's still active...
            tween.Kill();
        }

        tween = null;
    }
}
```

A disposable component processed with this plugin will have its `Dispose()` method
called when any of the following occurs:

- When [`Systems.TearDown()`](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/class_j_c_m_g_1_1_entitas_redux_1_1_systems.html#afead47632a2c36adafcbe23bd7fbba67) is called, usually when the game ends.
- When its owning entity is about to be destroyed, via the [`Context.OnEntityWillBeDestroyed`](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/class_j_c_m_g_1_1_entitas_redux_1_1_context.html#a912f923e4eb66d517a1c88bf5434de58) event.
- When it's removed, via the [`Group.OnEntityRemoved`](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/class_j_c_m_g_1_1_entitas_redux_1_1_group.html#a6c844f191107ac4b08c12c7a99b66694) event.
- When its value is changed, via the [`Group.OnEntityUpdated`](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/class_j_c_m_g_1_1_entitas_redux_1_1_group.html#a090c47e898895938faed4aa10b101b44) event. This currently occurs even if the component's value is replaced with itself.

## Configuration

To include it in your project, modify the following properties within `Genesis.properties` file like so:

```properties
Genesis.DataProviders = CorundumGames.Codegen.Redux.Plugins.DisposableComponent.DataProvider
Genesis.CodeGenerators = CorundumGames.Codegen.Redux.Plugins.DisposableComponent.Generator
```

## Usage

This plugin generates a `DisposeDataFeature` in `Generated/Features`.
Add it to your `Systems` instance to enable it in your project.
You may also add the generated systems to your `Systems` instances directly.

Disposable components may be added to as many `Context`s as you'd like.
This plugin will generate systems for each combination of component and context.

Entitas Redux pools components for reuse, therefore your `Dispose()` method **must not** make them unusable.
This means your disposable components must not use a `disposed` flag,
nor should they ever throw [`ObjectDisposedException`](https://docs.microsoft.com/en-us/dotnet/api/system.objectdisposedexception?view=netstandard-2.1).

## Compatibility

The plugin itself should work on all platforms that Genesis supports (Windows, macOS, Linux).

The generated code requires at least Unity 2021.1,
due to its use of [`UnityEngine.Pool`](https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Pool.ObjectPool_1).
If you want to use this generator without using `UnityEngine.Pool`,
please [open a ticket](https://github.com/CorundumGames/CorundumGames.Codegen/issues) (or even better, a pull request).

## Limits

generated systems are not in any namespaces
