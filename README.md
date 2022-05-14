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
When you do so, you'll need to add a specific directory to your `Jenny.SearchPaths` property. Add the following:

```properties
Jenny.SearchPaths = ... \
  Library/PackageCache/games.corundum.codegen.redux@0.1.9 \
  ... # Other directories
```

Adjust the version number depending on which one you have installed.

### Manually

This is the least convenient option, but it will work if you're unable to use the UPM package for some reason.

1. Download the latest `.nupkg` from NuGet [here](https://www.nuget.org/api/v2/package/CorundumGames.Codegen.Redux).
2. Extract the archive's contents using a tool that supports `.nupkg` files, such as [7-Zip](https://www.7-zip.org).
3. Copy the file `lib/net472/CorundumGames.Codegen.Redux.dll` to wherever you keep your codegen assemblies. If you're installing with this method, this will probably be in your project's source tree.
4. Add the directory containing your codegen assemblies to your `Jenny.properties` file.

## Configuration

To use any plugin in this repository, add the following to your `Jenny.properties` file:

```properties
Jenny.SearchPaths = ... # Configure as described in the Installation section

Jenny.Plugins = CorundumGames.Codegen
```

Configuration specific to each plugin can be found in the following sections, where applicable.
If you don't need a particular plugin, simply don't include it in your `Jenny.properties` file.

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
