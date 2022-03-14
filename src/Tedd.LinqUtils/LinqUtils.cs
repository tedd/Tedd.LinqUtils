using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Tedd;

public static class LinqUtils
{
    /// <summary>
    /// Counts number of items and executed action with integer of count when done.
    /// </summary>
    /// <param name="countAction">Action executed with number of items as parameter once count is done.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> CountTo<T>(this IEnumerable<T> source!!, Action<int> countAction!!)
    {
        var count = 0;
        foreach (var item in source)
        {
            count++;
            yield return item;
        }
        countAction.Invoke(count);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (List<T> OnlyInFirst, List<T> OnlyInSecond, List<T> InBoth) DeltaLists<T>(this IEnumerable<T> first!!, IEnumerable<T> second!!, IEqualityComparer<T>? comparer = null)
    {
        // We need to iterate data twice (creating hashset, IEnumerable won't allow that. So we copy data.
        var firstArray = first.ToArray();
        var secondArray = second.ToArray() ;
        
        // We will use hashset for fast lookup
        var firstHashSet = new HashSet<T>(firstArray, comparer);
        var secondHashSet = new HashSet<T>(secondArray, comparer);

        var onlyInFirst = new List<T>();
        var onlyInSecond = new List<T>();
        var common = new List<T>();

        // We iterate the array, not the hashset, since we want to add duplicates if they exist
        foreach (var item in firstArray)
        {
            if (secondHashSet.Contains(item))
                common.Add(item);
            else
                onlyInFirst.Add(item);                    
        }
        foreach (var item in secondArray)
        {
            if (firstHashSet.Contains(item))
                common.Add(item);
            else
                onlyInFirst.Add(item);                    
        }

        return (onlyInFirst, onlyInSecond, common);
    }

    public static (List<T> OnlyInFirst, List<T> OnlyInSecond, List<T> InBoth) DeltaListsUnique<T>(this IEnumerable<T> first!!, IEnumerable<T> second!!, IEqualityComparer<T>? comparer = null)
    {
        // We will use hashset for fast lookup
        var firstHashSet = new HashSet<T>(first, comparer);
        var secondHashSet = new HashSet<T>(second, comparer);

        var onlyInFirst = new List<T>();
        var onlyInSecond = new List<T>();
        var common = new List<T>();

        // We iterate the array, not the hashset, since we want to add duplicates if they exist
        foreach (var item in firstHashSet)
        {
            if (secondHashSet.Contains(item))
                common.Add(item);
            else
                onlyInFirst.Add(item);
        }
        foreach (var item in secondHashSet)
        {
            if (firstHashSet.Contains(item))
                common.Add(item);
            else
                onlyInFirst.Add(item);
        }

        return (onlyInFirst, onlyInSecond, common);
    }


}





