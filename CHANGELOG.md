# Changelog

## [1.4.1](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.4.0...1.4.1) (2023-08-08)


### Bug Fixes

* Make generated EntityBehaviour initializers internal so VContainer can use codegen on them ([fd919ae](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/fd919aea616e2a49839b7e257eb6feb2ce16e338))

# [1.4.0](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.3.1...1.4.0) (2022-10-25)


### Features

* Add plugins for generating cleanup systems that run in the fixed timestep ([81ac117](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/81ac117b5f2b44f68fb7e54b11fc9233e3d1f26c))

## [1.3.1](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.3.0...1.3.1) (2022-10-25)


### Bug Fixes

* Add a .meta file for FixedCleanupAttribute ([97bc214](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/97bc214a60dd23bfaac27d4dced9b8079570a044))

# [1.3.0](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.2.0...1.3.0) (2022-10-25)


### Features

* Add FixedCleanupAttribute ([9b34968](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/9b34968cc38b6b953e08a7a4ab38bbfaa4fbed09))

# [1.2.0](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.1.3...1.2.0) (2022-10-21)


### Features

* Add ComponentEntityApiInterfaceGenerator ([aaf54ae](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/aaf54aeb70f8eefbd73eac86af62e51a6b13e32f))

## [1.1.3](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.1.2...1.1.3) (2022-05-14)


### Bug Fixes

* Remove destructor for Context ([7bf272b](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/7bf272bace39d3eac2c378fa4f35e61c6f9982fd))
* Remove generated EntityIndexes method ([0ccae56](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/0ccae567ae03cd92cbdb86d5e913ed477ee136b0))

## [1.1.2](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.1.1...1.1.2) (2022-05-14)


### Bug Fixes

* Fix capitalization in DisposableContextsGeneratorTemplate ([558c304](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/558c30431dab684dff500ef91f8d17d85ab3622f))

## [1.1.1](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.1.0...1.1.1) (2022-05-14)


### Bug Fixes

* Fix a typo in DisposableContextsGenerator.Name ([82e4c05](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/82e4c054abb669d2f32e0c270c8b29f0acb2fac5))
* Fix an incorrect namespace reference in DisposableContextsGeneratorTemplate ([935603b](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/935603b77dc84a307715f4ca230e572edb0417e8))

# [1.1.0](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.0.3...1.1.0) (2022-05-14)


### Features

* Add DisposableContextsGenerator generator ([b60cd4e](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/b60cd4e2b4002d8f333f77b71b9fe86ff82183f9))

## [1.0.3](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.0.2...1.0.3) (2022-05-11)


### Bug Fixes

* Make the generated InitializeEnumIndices method public ([7eacdc3](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/7eacdc308d4f85488dd889a200c0f7aaa5a5139c))

## [1.0.2](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.0.1...1.0.2) (2022-05-09)


### Bug Fixes

* Empty commit to trigger GitHub Actions ([23ec0a6](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/23ec0a60d2e58541cf76a3ed7cea9da40ac3882c))

## [1.0.1](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/compare/1.0.0...1.0.1) (2022-05-09)


### Bug Fixes

* Fix incorrect csproj path in .releaserc.yml ([50f0044](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/50f00442e3bb16f165864588f84ec2e171454383))

Changelog

# 1.0.0 (2022-05-08)


### Bug Fixes

* Change a config key ([2438985](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/2438985705e6d00ffdae60409e9fa488409ec51e))
* Fix a compile error ([c90bf7c](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/c90bf7cd8dc3bdadc59640db85a0e3482b130369))
* Move DisposableComponent to the new namespace ([98969cd](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/98969cd94da0dcd3dac6fa168df3393b7dc22165))
* Mute potential null warnings ([dbfc434](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/dbfc434bbb3b699a377fb83392e3272d9f16b79c))
* Remove incorrect comment ([8431019](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/8431019d4d87784f49258774e98399e31deffe70))
* Rename Data to DisposableComponentData ([06c2af2](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/06c2af236f03b59a5f5517cb6f3509fd66d361d8))
* Rename Generator to DisposableComponentGenerator ([cb83d63](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/cb83d632200aa632e99b12ee3225000c591910ff))
* Rename the generated GameObjectComponent.gameObject field to value ([6393b85](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/6393b85d03eb3976baf68b3003ebda9d5076403c))
* Sort and dedupe #defines in GlobalDefinesTemplate ([0fcfb51](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/0fcfb515767753e4a362ea063ebad5f3268dad83))
* Target netcoreapp3.1 instead of net6.0 ([f9b66e3](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/f9b66e35ad2399780c5df0e5635d790cfae8434c))
* Update namespace use in DisposableContextGeneratorTemplate ([36ab014](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/36ab014b648001a0b80b4cc9e9a9f40a9c82498a))
* Use the correct casing for DisposableComponent's system template ([f456acc](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/f456acc3c7ed318569b1ebc9a3ba9185ea3665fa))


### Features

* Add EntityBehaviour ([b87fe89](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/b87fe89a6f66e564fb83eaafc334bdc7c4de181e))
* Add GameObjectComponent plugins ([7489660](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/74896602372f243705306dc99f696c9f71e95a30))
* Add GlobalDefines plugins ([7d61530](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/7d615303fc4e48a317f1478f3bb511477a27efef))
* Add InitFromUnityComponentAttribute ([cb41167](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/cb411673f63cc7bc7b1a6d631cfcd18302efbc91))
* Add plugin installer assets ([7ee9682](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/7ee9682e08bbc47c6713033198b1fe7a8c7d9501))
* Create CorundumGames.Codegen.Redux.Runtime ([50a916b](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/50a916bc5debc7ad7252bb0070dae26fdcd3ec82))
* Generate an EntityIndex in GameObjectComponentDataProvider ([61213b4](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/61213b4c7290c3211e8bd6e4cb669e77ff551acc))
* Port InitFromUnityComponent plugins ([4dcf72a](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/4dcf72a6f8fcebc0548bdd406d6671a3f5a21c52))
* Port some plugins from the old repo ([a7defd0](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/a7defd081b89e6a3750ac48bfe47eb8981c2fb8e))
* Remove IEntityBehaviour in favor of the generated blueprint interfaces ([50876d4](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/50876d42d5f4f3e53bec705014fb11c4c51a68c9))
* Rewrite the IndexByPlayer plugin to support any enum ([eca73a6](https://github.com/CorundumGames/CorundumGames.Codegen.Redux/commit/eca73a65b9ed5c4901789fb607222aa6267147c7))
