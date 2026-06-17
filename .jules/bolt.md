## 2024-06-16 - Optimization of DeltaLists

**Observation:**
`DeltaLists<T>` in `Tedd.LinqUtils` creates two arrays (`firstArray`, `secondArray`) via `.ToArray()` unconditionally. If the input `IEnumerable<T>` is already a list or array, this results in unnecessary allocations.

**Strategic Action:**
Replace `.ToArray()` with `first as IReadOnlyList<T> ?? first.ToArray()` or similar, so we can avoid O(N) allocation if the input is already materialized. Or better yet, we can check `TryGetNonEnumeratedCount`. Wait, if we just need to iterate it twice, maybe we can cast it to `IEnumerable<T>` if it's safe to iterate multiple times? No, we still need index or `foreach`. If we cast to `IReadOnlyCollection<T>`, we can avoid `.ToArray()` if it's already a collection. We will use `IReadOnlyCollection<T> ?? first.ToArray()`.

**Update:**
Allocations are reduced (Alloc Ratio = 0.86 for N=100000, saving ~780 KB), but mean time increased by 12% for N=100000. Wait, why?
Because `HashSet<T>` constructor is using `IEnumerable<T>`. When passing an array to `HashSet`, `HashSet` knows it's an array? Actually no, `IReadOnlyCollection<T>` check vs `.ToArray()`. If we do `.ToArray()`, `HashSet` uses `IEnumerable<T>`. But iterating an array in `foreach` is faster than enumerating `IReadOnlyCollection<T>` using `IEnumerator<T>`.

Let's do better: if it's already an array `T[]`, or `IList<T>`, we can iterate via loop, or let the runtime optimize it if we cast it to array/list directly? No, the `IReadOnlyCollection` iterates via `IEnumerable`, causing interface calls.

What if we do:
```csharp
        T[] firstArray = first as T[] ?? first.ToArray();
        T[] secondArray = second as T[] ?? second.ToArray();
```
If we use `first as T[]`, then `firstArray` is a strongly typed array! Then `foreach (var element in firstArray)` uses array indexing!
And we get both: avoid allocation if it's already an array, and fast `foreach`.
Wait, what if it's a `List<T>`?
```csharp
        var firstList = first as List<T>;
        // Or what if we use an intermediate helper?
```
But in most LINQ cases, it's either an array or we do `.ToArray()`.

Let's check if casting to `T[]` gives the performance back while avoiding allocation for arrays. Actually, many collections are `List<T>`. `first.ToArray()` guarantees an array.
Wait, if we use `ICollection<T>` to get Count:
```csharp
        int firstCount = first.TryGetNonEnumeratedCount(out int count1) ? count1 : -1;
```
Wait, we can't easily avoid allocation for `List<T>` without having `IList<T>` iteration.
If we do `List<T> list = first as List<T>; T[] arr = first as T[];` ...

What if we just use `.ToArray()`? Wait, if we use `.ToArray()`, the allocation is identical to Legacy. So what did I reduce? Oh, I used `first as IReadOnlyCollection<T> ?? first.ToArray()`. Because benchmark input was an array, it avoided `.ToArray()` allocation! This means 780 KB saved was the `int[100000]` array allocations!
But why was it slower? Because `IReadOnlyCollection<T>` interface iteration is virtual dispatch (IEnumerator), whereas iterating an array is direct.

So:
```csharp
        T[]? firstArray = first as T[];
        List<T>? firstList = firstArray == null ? first as List<T> : null;

        // No, maybe just:
        T[] firstArray = first as T[] ?? first.ToArray();
```
If the input is `List<T>`, `first.ToArray()` will allocate an array. Is that fine? Legacy ALWAYS allocated an array. So `first as T[] ?? first.ToArray()` will be strictly equal or better.
Wait, can we use `ReadOnlySpan<T>`? `CollectionsMarshal.AsSpan(list)`!

Let's test `first as T[] ?? first.ToArray()` to ensure we get both performance AND allocation reduction.

**Update 2:**
Success! Using `first as T[] ?? first.ToArray()` reduced allocations by ~14% (5740 KB -> 4960 KB) and reduced CPU time by ~12% (5.78ms -> 5.11ms) for N=100000.

I'll formulate the optimization via a PR request and provide the required verification.
