using System.Collections;
using System.Runtime.CompilerServices;

namespace Tedd;

public record struct ConditionalIEnumerable<T> : IEnumerable<T>
{
    internal readonly IEnumerable<T> Enumerable;
    internal readonly bool BranchClosed;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ConditionalIEnumerable(IEnumerable<T> enumerable, bool branchClosed)
    {
        Enumerable = enumerable;
        BranchClosed = branchClosed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator() => Enumerable.GetEnumerator();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator() => Enumerable.GetEnumerator();
}

public static class LinqConditionalMethods
{
    /// <summary>
    /// Conditionally adds linq method to the linq chain.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="enabled">True if linq statement is added</param>
    /// <param name="linqAction">Linq method to add.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConditionalIEnumerable<T> If<T>(this IEnumerable<T> source!!, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (enabled)
            source = linqAction(source);
        return new ConditionalIEnumerable<T>(source, enabled);
    }

    /// <summary>
    /// Conditionally adds linq method to the linq chain.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="enabled">True if linq statement is added</param>
    /// <param name="linqAction">Linq method to add.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConditionalIEnumerable<T> ElseIf<T>(this ConditionalIEnumerable<T> source, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (!source.BranchClosed && enabled)
            return new ConditionalIEnumerable<T>(linqAction(source.Enumerable), true);
        return source;
    }

    /// <summary>
    /// Conditionally adds linq method to the linq chain.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="linqAction">Linq method to add.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Else<T>(this ConditionalIEnumerable<T> source, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (!source.BranchClosed)
            return linqAction(source.Enumerable);
        return source.Enumerable;
    }
}


