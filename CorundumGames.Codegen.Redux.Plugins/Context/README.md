# Context

The `Context` namespace consist of assorted `Context`-related plugins
plugins that dont go with anything else

## `AttributeGenerator`

TODO description

### Configuration

### Usage

### Known Issues

no namespaces

## `DisposableContextGenerator`

Generates code that makes each context implement `IDisposable`.

### Configuration

None specific to this plugin; code is generated for all contexts.

### Usage

When you're done with your contexts, call the dispose method and all entities will be destroyed

You'll likely use this with DisposableContextsGenerator, but you can dispose the contexts individually if you want

### Known Issues

no namespaces

### See Also

doesnt enforce disposed flag outside of the dispose method itself

## `TypeEnumGenerator`

TODO description

### Configuration

### Usage

### Known Issues

no namespaces
