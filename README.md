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

As far as assemblies go, this project works a little differently.
The assembly is not intended to be used by Unity directly; it is intended for use by the Genesis executable.

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

At the moment, this repository only includes one plugin.
However, I may add more that I develop and use if they're interesting enough to reuse across projects.
If necessary, I will split these plugins into multiple assemblies within this source repository.


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
