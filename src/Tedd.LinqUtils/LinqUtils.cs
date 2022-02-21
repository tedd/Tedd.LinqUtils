using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Tedd
{
    public static class LinqUtils
    {
        /// <summary>
        /// Perform an action for each item in collection. Same as ForEach.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">Action to execute for each item in collection.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Action<T>(this IEnumerable<T>? source, Action<T> action) => ForEach(source, action);
        /// <summary>
        /// Executed action for each item in collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">Action to execute for each item in collection.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T>? source, Action<T> action)
        {
            foreach (var element in source!)
                action(element);
            return source;
        }

        /// <summary>
        /// Joins strings with separator. Same as wrapping collection in String.Join().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator">Separator to put between elements.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string JoinStrings<T>(this IEnumerable<T>? source, string separator) => string.Join(separator, source!);
        /// <summary>
        /// Joins strings with separator. Same as wrapping collection in String.Join().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator">Separator to put between elements.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string JoinStrings<T>(this IEnumerable<T>? source, char separator) => string.Join(separator, source!);

        public static IEnumerable<T> Minus<T>(this IEnumerable<T>? source, IEnumerable<T> other)
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
        public static IEnumerable<T> Unique<T>(this IEnumerable<T>? source, IEqualityComparer<T> equalityComparer = null) => new HashSet<T>(source!, equalityComparer);


        public static IEnumerable<T> Plus<T>(this IEnumerable<T>? source, IEnumerable<T> other)
        {
            var set = new List<T>(source!);
            set.AddRange(other);
            return set;
        }

        public static IEnumerable<T> PlusUnique<T>(this IEnumerable<T>? source, IEnumerable<T> other, IEqualityComparer<T> equalityComparer = null)
        {
            var set = new HashSet<T>(source!, equalityComparer);
            foreach (var element in other)
                if (!set.Contains(element))
                    set.Add(element);
                else
                    set.Remove(element);
            return set;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T>? source) => source!;
        /// <summary>
        /// Same as: .Where(w => w != null)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T>? source) => source!.Where(w => w != null);
        /// <summary>
        /// Same as: .Where(w => !string.IsNullOrEmpty(w)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> WhereIsNotNullOrEmpty(this IEnumerable<string>? source) => source!.Where(w => !string.IsNullOrEmpty(w));
        /// <summary>
        /// Same as: .Where(w => !string.IsNullOrWhiteSpace(w)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> WhereIsNotNullOrWhiteSpace(this IEnumerable<string>? source) => source!.Where(w => !string.IsNullOrWhiteSpace(w));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? If<T>(this IEnumerable<T>? source, bool enabled, Func<IEnumerable<T>, IEnumerable<T>> linqAction)
            => enabled ? linqAction(source!) : source;

        public static Queue<T> ToQueue<T>(this IEnumerable<T>? source) => new Queue<T>(source);
        public static Stack<T> ToStack<T>(this IEnumerable<T>? source) => new Stack<T>(source);
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T>? source) => new HashSet<T>(source);
        public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T>? source) => new ConcurrentQueue<T>(source);
        public static ConcurrentStack<T> ToConcurrentStack<T>(this IEnumerable<T>? source) => new ConcurrentStack<T>(source);
        public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T>? source) => new ConcurrentBag<T>(source);

        /// <summary>
        /// Shufle items randomly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="useCryptoGradeRandom">Uses crypto grade random by utilizing the operating systems underlying CSP (Cryptographic Service Provider) for better random data.</param>
        /// <returns></returns>
        public unsafe static IEnumerable<T> Shuffle<T>(this IEnumerable<T>? source, bool useCryptoGradeRandom = false)
        {
            Random? rnd = null;
            RandomNumberGenerator? rng = null;
            Span<byte> bytes = stackalloc byte[4];
            if (useCryptoGradeRandom)
                rng = RandomNumberGenerator.Create();
            else
                rnd = new Random();

            var items = source!.ToArray();
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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    j = rnd.Next(0, items.Length);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                items[j] = items[i];
            }

            rng?.Dispose();

            return items;
        }
    }
}