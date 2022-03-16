using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tedd;

public static class LinqActionMethods
{
    /// <summary>
    /// Perform an action for each item in collection. Same as ForEach.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action">Action to execute for each item in collection.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Action<T>(this IEnumerable<T> source!!, Action<T> action!!) => ForEach(source, action);
    /// <summary>
    /// Executed action for each item in collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action">Action to execute for each item in collection.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source!!, Action<T> action!!)
    {
        foreach (var element in source)
        {
            action(element);
            yield return element;
        }
    }
}

