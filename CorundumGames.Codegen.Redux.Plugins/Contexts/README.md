## Known Issues

Deletes all `ContextObserverBehaviour`s, not just the one associated with the contained contexts.
This will be a problem if you have multiple `Contexts` objects alive at once.
