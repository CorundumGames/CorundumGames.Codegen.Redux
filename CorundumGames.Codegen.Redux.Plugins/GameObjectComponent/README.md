# `CorundumGames.Codegen.Redux.Plugins.GameObjectComponent`

This plugin generates `CodeGeneratorData` that represents a GameObjectComponent and an accompanying PrimaryEntityIndex.
The generated data can be used by any code generation plugin as if the `GameObjectComponent` was written by hand.

An earlier draft of this plugin generated the text of `GameObjectComponent` directly, but this required all codegen invocations to run twice
(once to generate the component, once more to use it for other codegen plugins).

## Configuration

list the contexts with the following key

## Usage

Add the `CorundumGames.Codegen.Redux.Plugins.GameObjectComponent.GameObjectComponentDataProvider` plugin to your config file's list of data providers, like so:

```properties
Genesis.DataProviders = CorundumGames.Codegen.Redux.Plugins.GameObjectComponent.GameObjectComponentDataProvider
```

The generated code will look something like this:

```c#
[JCMG.EntitasRedux.DontGenerate(false)]
public sealed class GameObjectComponent : JCMG.EntitasRedux.IComponent
{
	public UnityEngine.GameObject value;
}
```

will also create a primaryentityindex for each listed context

You can use the generated component just as you would if you'd hand-written it, but its intended for use by other plugins in this project

## Compatibility

The plugin should work on all platforms that Genesis supports (Windows, macOS, Linux).

this plugin doesn't generate code, it generates data

The code that this plugin generates will not be in the same assembly as your hand-written components.
if you need to ensure its in the same assembly as your hand-written components, use an asmref

## Limits

does not generate with namespaces.
