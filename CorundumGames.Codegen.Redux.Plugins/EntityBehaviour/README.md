# `CorundumGames.Codegen.Redux.Plugins.EntityBehaviour`

This plugin has data providers for a gameobjectcomponent and an entity index

## Configuration

list the contexts with the following key

## Usage

use the gameobjectcomponent

intended for use with the other plugins in this repo

output will look like this

```c#
[JCMG.EntitasRedux.DontGenerate(false)]
public sealed class GameObjectComponent : JCMG.EntitasRedux.IComponent
{
	public UnityEngine.GameObject value;
}
```

will also create a primaryentityindex for each listed context

## Compatibility

The plugin itself should work on all platforms that Genesis supports (Windows, macOS, Linux).

The generated code requires at least Unity 2021.1,
due to its use of [`UnityEngine.Pool`](https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Pool.ObjectPool_1).
If you want to use this generator without using `UnityEngine.Pool`,
please [open a ticket](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/issues)
(or even better, a pull request).

The generated code also requires VContainer, as it's intended for use in a project that relies on dependency injection.


## Dependencies

This plugin is intended to be used alongside the following others:

- `CorundumGames.Codegen.Redux.Plugins.GameObjectComponent.GameObjectComponentDataProvider`
- `EntitasRedux.Core.Plugins.ComponentEntityApiInterfaceGenerator`
- `EntitasRedux.Unity.BlueprintPlugins.BlueprintClassGenerator`

Variants of these plugins that produce identical output are acceptable.

## Limits

does not generate with namespaces.
