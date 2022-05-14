# CorundumGames.Codegen.Redux

[![Nuget](https://img.shields.io/nuget/v/CorundumGames.Codegen.Redux?style=for-the-badge)](https://www.nuget.org/packages/CorundumGames.Codegen.Redux)
[![openupm](https://img.shields.io/npm/v/games.corundum.codegen.redux?label=openupm&registry_uri=https://package.openupm.com&style=for-the-badge)](https://openupm.com/packages/games.corundum.codegen.redux)

A set of [Genesis](https://github.com/jeffcampbellmakesgames/Genesis) plugins
that I think are useful for a wide variety of projects that use C#, Unity, and [Entitas Redux](https://github.com/jeffcampbellmakesgames/Entitas-Redux).

# How to Use

These plugins are intended to be used directly by the Genesis code generator.
They are not intended to be used as a dependency for your own plugins,
nor are they intended for use within the Unity editor.

## Installation

This section describes the ways in which you can add these plugins to your project.

### Via OpenUPM

Install the package `games.corundum.codegen.redux` from OpenUPM through the instructions described [here](https://openupm.com/packages/games.corundum.codegen.redux/#modal-manualinstallation).


## Configuration

Your next steps then depend on how you manage your project's Genesis configuration.
Each plugin's respective `README.md` file lists configuration guidance.
The following sections describe configuration that is common to all included plugins.

### Via `.properties` Files

If you configure Genesis with `.properties` files (similar to Jenny),
you can use these plugins by modifying the following properties.

```properties
# Add the name of the assembly somewhere in the comma-separated list (line continuations are OK)
Genesis.Plugins = ... \
  CorundumGames.Codegen.Redux.Plugins, \
  ...
```

Settings for this repo's plugins are prefixed with `CorundumGames.Codegen.Redux.Plugins`
unless otherwise noted.

If you don't need a particular plugin within the provided assembly,
simply don't include it in your configuration.

# Plugins

This repo includes the following plugins, each of which has a `README.md` file in its source directory:

- [**Context**](CorundumGames.Codegen.Redux.Plugins/Context/README.md):
  Assorted plugins that augment or rely on
  [`Context`s](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/class_j_c_m_g_1_1_entitas_redux_1_1_context).
- [**Contexts**](CorundumGames.Codegen.Redux.Plugins/Contexts/README.md):
  Assorted plugins that augment or rely on
  [`Contexts`](https://jeffcampbellmakesgames.github.io/Entitas-Redux-API/interface_j_c_m_g_1_1_entitas_redux_1_1_i_contexts)
  (note the extra `s`).
- [**Disposable Component**](CorundumGames.Codegen.Redux.Plugins/DisposableComponent/README.md):
  Systems that clean up any [`IDisposable`](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netstandard-2.1) component
  when it's removed or replaced.
- [**`EntityBehaviour`**](CorundumGames.Codegen.Redux.Plugins/EntityBehaviour/README.md):
  Generated `MonoBehaviour`s for `Entity`s.
- [**`GameObject` Component**](CorundumGames.Codegen.Redux.Plugins/GameObjectComponent/README.md):
  Generate a component that contains a [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject).
  Mostly used as a prerequisite for other plugins.
- [**Global Defines**](CorundumGames.Codegen.Redux.Plugins/GlobalDefines/README.md):
  List the project-wide `#define` symbols that your game uses, to aid in debugging.
- [**Index By Enum**](CorundumGames.Codegen.Redux.Plugins/IndexByEnum/README.md):
  Generate an `EntityIndex` that makes a given component indexable by *another* component that contains an `enum`.

# Building

This project can be built and used on Windows, macOS, and Linux.
Install the latest version of .NET and build it on the command-line as follows:W

```shell
# Change directory to the cloned repo...
cd CorundumGames.Codegen.Redux

# Install all dependencies...
dotnet restore
dotnet tool restore

# ...then build and package the libraries.
dotnet build
```

If you have [`act`](https://github.com/nektos/act) installed,
you can run [the GitHub Actions workflow](.github/workflows/build.yml) locally
by running the `act` command within the repo's root.

# Contributing

This repository is primarily written for my own needs,
but I'm happy to receive bug reports or pull requests.

# License

All files in this repository are released under the MIT license, unless otherwise noted.

You own the output of each plugin,
I make no ownership claims to it.
