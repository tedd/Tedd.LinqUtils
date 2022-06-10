using System.Collections.Concurrent;

namespace Tedd;

public static class LinqToCollectionMethods
{
    // TODO: Support capacity parameter?
    /// <summary>
    /// Convert to Queue&lt;T&gt;.
    /// </summary>
    /// <returns>Queue&lt;T&gt; object containing all elements.</returns>
    public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new Queue<T>(source);
    }

    /// <summary>
    /// Convert to Stack&lt;T&gt;.
    /// </summary>
    /// <returns>Stack&lt;T&gt; object containing all elements.</returns>
    public static Stack<T> ToStack<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new Stack<T>(source);
    }

    /// <summary>
    /// Convert to HashSet&lt;T&gt;.
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns>HashSet&lt;T&gt; object containing all elements.</returns>
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new HashSet<T>(source);
    }

    /// <summary>
    /// Convert to HashSet&lt;T&gt;.
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns>HashSet&lt;T&gt; object containing all elements.</returns>
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new HashSet<T>(source, comparer);
    }

    /// <summary>
    /// Convert to ConcurrentQueue&lt;T&gt;.
    /// </summary>
    /// <returns>ConcurrentQueue&lt;T&gt; object containing all elements.</returns>
    public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new ConcurrentQueue<T>(source);
    }

    /// <summary>
    /// Convert to ConcurrentStack&lt;T&gt;.
    /// </summary>
    /// <returns>ConcurrentStack&lt;T&gt; object containing all elements.</returns>
    public static ConcurrentStack<T> ToConcurrentStack<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new ConcurrentStack<T>(source);
    }

    /// <summary>
    /// Convert to ConcurrentBag&lt;T&gt;.
    /// </summary>
    /// <returns>ConcurrentBag&lt;T&gt; object containing all elements.</returns>
    public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return new ConcurrentBag<T>(source);
    }
}

