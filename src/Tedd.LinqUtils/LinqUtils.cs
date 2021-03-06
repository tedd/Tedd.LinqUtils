using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

[assembly:CLSCompliant(true)]
namespace Tedd;

public static class LinqUtils
{
    /// <summary>
    /// Counts number of items and executed action with integer of count when done.
    /// </summary>
    /// <param name="count">Action executed with number of items as parameter once count is done.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> CountTo<T>(this IEnumerable<T> source, Action<int> count)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (count == null) throw new ArgumentNullException(nameof(count));
        var counter = 0;
        foreach (var item in source)
        {
            counter++;
            yield return item;
        }
        count.Invoke(counter);
    }


}





