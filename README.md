# Tedd.LinqUtils

Various useful LINQ extension methods.

* Full code coverage on unit tests.
* Attempt at minimizing allocations.
* Optimized for large collections in mind, streaming wherever possible.

## Architectural Paradigm

The framework operates via stateless, functional LINQ extension methods executing against `IEnumerable<T>`.  Capabilities prioritize lazy evaluation and streaming to minimize memory allocations when processing extensive data structures.  The framework explicitly **does not** utilize hierarchical data binding or routed event infrastructure; execution is deterministic and sequence-dependent.

## Action and Utility methods

Perform an action on a collection, forwarding the collection in the chain, or count items efficiently.

```c#
// ForEach and Action execute a delegate per item while streaming the collection.
collection
    .ForEach(item => foobar(item))
    .Action(item => anotherfoobar(item));
// Would be same as
collection
    .Select(item => { foobar(item); return item; })
    .Select(item => { anotherFoobar(item); return item; });

// Count items and trigger a callback upon completion of the enumeration
var result = collection.CountTo(count => Console.WriteLine($"Processed {count} items"));
```

## Conditional methods

Allows conditional branching of the LINQ query itself.

```c#
// Will add LINQ-method to filter on AuthorId if userlevel != Level.Admin
var result = collection
    .If(userLevel != Level.Admin, x => x.Where(w => w.AuthorId == userId))
    .ToArray();

// Supports If, unlimited ElseIf, and Else
var result = collection
    .If(userLevel == Level.Admin, x => x)
    .ElseIf(userLevel == Level.SuperUser, x => x.Where(w => w.GroupId == groupId))
    .ElseIf(userLevel == Level.Moderator, x => x.Where(w => w.GroupId == groupId))
    .Else(x => x.Where(w => w.AuthorId == userId))
    .Select(x => x.Title);
```

## Filter methods

Filters collection based on null or string.IsNotNullOrEmpty / string.IsNotNullOrWhiteSpace.

```c#
var notNulls = collection.WhereIsNotNull().ToArray();
var notNulls = stringCollection.WhereIsNotNullOrEmpty().ToArray();
var notNulls = stringCollection.WhereIsNotNullOrWhiteSpace().ToArray();
```

## List manipulation methods

```c#
// Remove items in one collection from another
var remaining = collection.Remove(new int[] { 1, 2, 3 });
var remaining = collection.Remove(otherCollection, customComparer);

// Append or prepend to collection
var appended = collection.Append(element);
var appended = collection.Append(otherCollection);
var prepended = collection.Prepend(otherCollection);

// Add only items that do not already exist.
// Note: Requires keeping full collection in memory, but will stream.
var includesMissing = collection.AppendDistinct(otherCollection);
var includesMissing = collection.AppendDistinct(otherCollection, comparer);

// Shuffle / randomize order of a collection
// Note: Requires loading of full collection to memory.
var shuffled = collection.Shuffle();

// Calculate difference between two lists
// Note: Requires loading of full collection to memory.
var delta = firstCollection.DeltaLists(secondCollection);
var deltaWithComparer = firstCollection.DeltaLists(secondCollection, customComparer);
var deltaWithSelector = firstCollection.DeltaLists(secondCollection, x => x.Id);
// delta object of type DeltaResult contains lists OnlyInFirst, OnlyInSecond and InBoth
```

## String methods

```c#
// string.Join collection
var str = collection.StringJoin(", ");
var str = collection.StringJoin(':');
```

## Collection methods

Additions to .ToArray(), .ToList() and .ToDictionary().

```c#
var queue = collection.ToQueue();
var stack = collection.ToStack();
var hashSet = collection.ToHashSet();
var hashSet = collection.ToHashSet(comparer);
var concurrentQueue = collection.ToConcurrentQueue();
var concurrentStack = collection.ToConcurrentStack();
var concurrentBag = collection.ToConcurrentBag();
```