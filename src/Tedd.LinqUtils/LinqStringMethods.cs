﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tedd;

public static class LinqStringMethods
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<string> StartsWithString(this IEnumerable<string> source!!, string str!!)
    //{
    //    yield return str;
    //    foreach (var item in source)
    //        yield return item;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<string> EndsWithString(this IEnumerable<string> source!!, string str!!)
    //{
    //    foreach (var item in source)
    //        yield return item;
    //    yield return str;
    //}
    /// <summary>
    /// Joins strings with separator. Same as wrapping collection in String.Join().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator">Separator to put between elements.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string StringJoin<T>(this IEnumerable<T> source!!, string separator!!) => string.Join(separator, source);
    /// <summary>
    /// Joins strings with separator. Same as wrapping collection in String.Join().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator">Separator to put between elements.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string StringJoin<T>(this IEnumerable<T> source!!, char separator) => string.Join(separator, source);

}
