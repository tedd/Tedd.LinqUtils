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
    /// <param name="count">Action executed with number of items as parameter once count is done.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> CountTo<T>(this IEnumerable<T> source!!, Action<int> count!!)
    {
        var count = 0;
        foreach (var item in source)
        {
            count++;
            yield return item;
        }
        count.Invoke(count);
    }


}





