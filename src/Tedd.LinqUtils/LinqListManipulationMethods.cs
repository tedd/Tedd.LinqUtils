using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Tedd;

public static class LinqListManipulationMethods
{
    /// <summary>
    /// Remove all elements that exist in other list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to remove.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!) => source.Remove(other, null);

    /// <summary>
    /// Remove all elements that exist in other list using custom comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to remove.</param>
    /// <param name="comparer">Custom comparer.</param>
    /// <returns></returns>
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!, IEqualityComparer<T>? comparer)
    {
        var set = new HashSet<T>(other, comparer);
        foreach (var element in source)
        {
            if (!set.Contains(element))
                yield return element;
        }
    }
    /// <summary>
    /// Append element to end of list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">Element to append.</param>
    /// <returns></returns>
    public static IEnumerable<T> Append<T>(this IEnumerable<T> source!!, T other!!)
    {
        foreach (var element in source)
            yield return element;
        yield return other;
    }

    /// <summary>
    /// Append elements to end of list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to append.</param>
    /// <returns></returns>
    public static IEnumerable<T> Append<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!)
    {
        foreach (var element in source)
            yield return element;
        foreach (var element in other)
            yield return element;
    }

    /// <summary>
    /// Prepend elements to start of list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to prepend.</param>
    /// <returns></returns>
    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!)
    {
        foreach (var element in other)
            yield return element;
        foreach (var element in source)
            yield return element;
    }


    /// <summary>
    /// Append elements only if they do not already exist in list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to append.</param>
    /// <returns></returns>
    public static IEnumerable<T> AppendDistinct<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!) => source.AppendDistinct(other, null);
    /// <summary>
    /// Append elements only if they do not already exist in list using custom comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">List of elements.</param>
    /// <param name="other">List of elements to append.</param>
    /// <param name="comparer">Custom comparer.</param>
    /// <returns></returns>
    public static IEnumerable<T> AppendDistinct<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!, IEqualityComparer<T>? comparer)
    {
        var set = new HashSet<T>(comparer);
        foreach (var element in source)
            if (set.Add(element))
                yield return element;

        foreach (var element in other)
            if (set.Add(element))
                yield return element;
    }

    /// <summary>
    /// Shuffle elements randomly.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="useCryptoGradeRandom">Use crypto grade random by utilizing the operating systems underlying CSP (Cryptographic Service Provider) for better random data.</param>
    /// <returns></returns>
    public static unsafe IEnumerable<T> Shuffle<T>(this IEnumerable<T> source!!, bool useCryptoGradeRandom = false)
    {
        Random? rnd = null;
        RandomNumberGenerator? rng = null;
        Span<byte> bytes = stackalloc byte[4];
        if (useCryptoGradeRandom)
            rng = RandomNumberGenerator.Create();
        else
            rnd = new Random();

        IList<T> elements;
        if (source is IList<T>)
            elements = (IList<T>)source;
        else
            elements = source.ToArray();

        for (var i = 0; i < elements.Count; i++)
        {
            var j = 0;
            if (useCryptoGradeRandom)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                UInt32 ri = 0;
                do
                {
                    rng.GetBytes(bytes);
                    ri = MemoryMarshal.Cast<byte, UInt32>(bytes)[0];
                } while (ri == UInt32.MaxValue);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                j = (int)((double)ri / (double)UInt32.MaxValue * (double)elements.Count);
            }
            else
#pragma warning disable CA5394 // Do not use insecure randomness
                j = rnd!.Next(0, elements.Count);
#pragma warning restore CA5394 // Do not use insecure randomness
            (elements[j], elements[i]) = (elements[i], elements[j]);
        }

        rng?.Dispose();

        return elements;
    }

    ///// <summary>
    ///// Calculates delta lists of two lists.
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="first">First list of elements.</param>
    ///// <param name="second">Second list of elements.</param>
    ///// <returns>OnlyInFirst, OnlyInSecond, InBoth</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static DeltaResult<T> DeltaLists<T>(this IEnumerable<T> first, IEnumerable<T> second)
    //{
    //    if (first == null) throw new ArgumentNullException(nameof(first));
    //    if (second == null) throw new ArgumentNullException(nameof(second));
    //    return first.DeltaLists(second, null);
    //}

    /// <summary>
    /// Calculates delta lists of two lists using custom comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first">First list of elements.</param>
    /// <param name="second">Second list of elements.</param>
    /// <param name="comparer">Optional custom comparer.</param>
    /// <returns>OnlyInFirst, OnlyInSecond, InBoth</returns>
    public static DeltaResult<T> DeltaLists<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T>? comparer = null)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        // We need to iterate data twice (creating hashset, IEnumerable won't allow that. So we copy data.
        var firstArray = first.ToArray();
        var secondArray = second.ToArray();

        // We will use hashset for fast lookup
        var firstHashSet = new HashSet<T>(firstArray, comparer);
        var secondHashSet = new HashSet<T>(secondArray, comparer);

        // Allocate max memory possible, we'll trim at the end. This to avoid too many reallocations.
        var onlyInFirst = new List<T>(firstArray.Length);
        var onlyInSecond = new List<T>(secondArray.Length);
        var common = new List<T>(Math.Max(firstArray.Length, secondArray.Length));

        // We iterate the array, not the hashset, since we want to add duplicates if they exist
        foreach (var element in firstArray)
        {
            if (secondHashSet.Contains(element))
                common.Add(element);
            else
                onlyInFirst.Add(element);
        }
        foreach (var element in secondArray)
        {
            if (firstHashSet.Contains(element))
                common.Add(element);
            else
                onlyInFirst.Add(element);
        }

        // Trim away excess memory
        onlyInFirst.TrimExcess();
        onlyInSecond.TrimExcess();
        common.TrimExcess();

        return new (onlyInFirst, onlyInSecond, common);
    }

}


