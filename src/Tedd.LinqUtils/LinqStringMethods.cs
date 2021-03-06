using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tedd;

public static class LinqStringMethods
{
    /// <summary>
    /// Joins strings with separator. Same as wrapping collection in String.Join().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator">Separator to put between elements.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string StringJoin<T>(this IEnumerable<T> source, string separator)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (separator == null) throw new ArgumentNullException(nameof(separator));
        return string.Join(separator, source);
    }

    /// <summary>
    /// Joins strings with separator. Same as wrapping collection in String.Join().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator">Separator to put between elements.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string StringJoin<T>(this IEnumerable<T> source, char separator)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return string.Join(separator, source);
    }
    ///// <summary>
    ///// Same as: .Select(w => Trim(w))
    ///// </summary>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<string> StringTrim(this IEnumerable<string> source!!) => source.Where(w => !string.IsNullOrWhiteSpace(w));

}

