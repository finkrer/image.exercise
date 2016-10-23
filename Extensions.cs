using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class Extensions
    {
        public static TResult[,] Select<TSource, TResult>
            (this TSource[,] array, Func<TSource[,], int, int, TResult> selector)
        {
            var result = new TResult[array.GetLength(0), array.GetLength(1)];
            for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    result[i, j] = selector(array, i, j);
            return result;
        }

        public static TResult[,] Select<TSource, TResult>
            (this TSource[,] array, Func<TSource, TResult> selector)
        {
            return array.Select((a, i, j) => selector(a[i, j]));
        }

        public static TResult[,] Zip<TSource1, TSource2, TResult>
        (this TSource1[,] array1, TSource2[,] array2,
            Func<TSource1[,], TSource2[,], int, int, TResult> selector)
        {
            if (array1.GetLength(0) != array2.GetLength(0)
                || array1.GetLength(1) != array2.GetLength(1))
                throw new ArgumentException();
            var result = new TResult[array1.GetLength(0), array1.GetLength(1)];
            for (var i = 0; i < array1.GetLength(0); i++)
                for (var j = 0; j < array1.GetLength(1); j++)
                    result[i, j] = selector(array1, array2, i, j);
            return result;
        }

        public static TResult[,] Zip<TSource1, TSource2, TResult>
        (this TSource1[,] array1, TSource2[,] array2,
            Func<TSource1, TSource2, TResult> selector)
        {
            return array1.Zip(array2, (a, b, i, j) => selector(a[i, j], b[i, j]));
        }

        public static TSource[,] TakeBlock<TSource>
            (this TSource[,] array, int left, int right, int down, int up)
        {
            if (left < 0 || right >= array.GetLength(0) || left > right
                || down < 0 || up >= array.GetLength(1) || down > up)
                throw new ArgumentException();
            var result = new TSource[right - left + 1, up - down + 1];
            for (var i = left; i <= right; i++)
                for (var j = down; j <= up; j++)
                    result[i - left, j - down] = array[i, j];
            return result;
        }

        public static IEnumerable<TSource> ToIEnumerable<TSource>(this TSource[,] array)
        {
            return array.Cast<TSource>();
        }
    }
}
