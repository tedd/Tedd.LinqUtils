using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Tedd;

public static class LinqListManipulationMethods
{
    public static IEnumerable<T> Minus<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!)
    {
        var set = new HashSet<T>(source!);
        foreach (var element in other)
            if (set.Contains(element))
                set.Remove(element);
            else
                set.Add(element);
        return set;
    }

    /// <summary>
    /// Removes duplicated objects from collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Unique<T>(this IEnumerable<T> source!!, IEqualityComparer<T>? equalityComparer = null) => new HashSet<T>(source, equalityComparer);

    public static IEnumerable<T> Append<T>(this IEnumerable<T> source!!, T other!!)
    {
        foreach (var item in source)
            yield return item;
        yield return other;
    }
    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source!!, T other!!)
    {
        yield return other;
        foreach (var item in source)
            yield return item;
    }

    public static IEnumerable<T> Plus<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!)
    {
        foreach (var item in source)
            yield return item;
        foreach (var item in other)
            yield return item;
    }

    public static IEnumerable<T> PlusUnique<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!, IEqualityComparer<T>? equalityComparer = null)
    {
        var set = new HashSet<T>(source, equalityComparer);
        foreach (var element in other)
            if (!set.Contains(element))
                set.Add(element);
            else
                set.Remove(element);
        return set;
    }
    //public static IEnumerable<T> PlusUnique<T>(this IEnumerable<T> source!!, IEnumerable<T> other!!, Func<T, T, bool> equalityComparer!!)
    //{
    //    //First check all in a against all in b. Any not in a or not in b goes in. The rest depents on comparer.

    //    var set = new HashSet<T>(source, equalityComparer);
    //    foreach (var element in other)
    //        if (!set.Contains(element))
    //            set.Add(element);
    //        else
    //            set.Remove(element);
    //    return set;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T>? If<T>(this IEnumerable<T> source!!, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction!!) => enabled ? linqAction(source) : source;
// TODO: IFELSE ELSE

    /// <summary>
    /// Shufle items randomly.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="useCryptoGradeRandom">Uses crypto grade random by utilizing the operating systems underlying CSP (Cryptographic Service Provider) for better random data.</param>
    /// <returns></returns>
    public unsafe static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source!!, bool useCryptoGradeRandom = false)
    {
        Random? rnd = null;
        RandomNumberGenerator? rng = null;
        Span<byte> bytes = stackalloc byte[4];
        if (useCryptoGradeRandom)
            rng = RandomNumberGenerator.Create();
        else
            rnd = new Random();

        var items = source.ToArray();
        for (var i = 0; i < items.Length; i++)
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
                j = (int)((double)ri / (double)UInt32.MaxValue * (double)items.Length);
            }
            else
#pragma warning disable CA5394 // Do not use insecure randomness
                j = rnd!.Next(0, items.Length);
#pragma warning restore CA5394 // Do not use insecure randomness
            items[j] = items[i];
        }

        rng?.Dispose();

        return items;
    }
}


