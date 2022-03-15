using System.Collections;
using System.Runtime.CompilerServices;

namespace Tedd;

public struct ConditionalIEnumerable<T> : IEnumerable<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConditionalIEnumerable<T> If<T>(this IEnumerable<T> source!!, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (enabled)
            source = linqAction(source);
        return new ConditionalIEnumerable<T>(source, enabled);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConditionalIEnumerable<T> ElseIf<T>(this ConditionalIEnumerable<T> source, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (!source.BranchClosed && enabled)
            return new ConditionalIEnumerable<T>(linqAction(source.Enumerable), true);
        return source;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Else<T>(this ConditionalIEnumerable<T> source, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!)
    {
        if (!source.BranchClosed)
            return linqAction(source.Enumerable);
        return source.Enumerable;
    }


}


