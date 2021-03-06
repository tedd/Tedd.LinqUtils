using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tedd;

public static class LinqFilterMethods
{
    /// <summary>
    /// Same as: .Where(w => w != null)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> WhereIsNotNull<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.Where(w => w != null);
    }

    /// <summary>
    /// Same as: .Where(w => !string.IsNullOrEmpty(w)
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> WhereIsNotNullOrEmpty(this IEnumerable<string> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.Where(w => !string.IsNullOrEmpty(w));
    }

    /// <summary>
    /// Same as: .Where(w => !string.IsNullOrWhiteSpace(w)
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> WhereIsNotNullOrWhiteSpace(this IEnumerable<string> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.Where(w => !string.IsNullOrWhiteSpace(w));
    }
}
