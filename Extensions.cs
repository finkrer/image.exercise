using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Recognizer
{
    internal static class Extensions
    {
        public static TResult[,] Select<TSource, TResult>
            (this TSource[,] matrix, Func<TSource[,], int, int, TResult> selector)
        {
            var result = new TResult[matrix.GetLength(0), matrix.GetLength(1)];
            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = 0; j < matrix.GetLength(1); j++)
                    result[i, j] = selector(matrix, i, j);
            return result;
        }

        public static TResult[,] Select<TSource, TResult>
            (this TSource[,] matrix, Func<TSource, TResult> selector)
        {
            return matrix.Select((m, i, j) => selector(m[i, j]));
        }

        public static TResult[,] Zip<TSource1, TSource2, TResult>
        (this TSource1[,] matrix1, TSource2[,] matrix2,
            Func<TSource1[,], TSource2[,], int, int, TResult> selector)
        {
            var result = new TResult[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (var i = 0; i < matrix1.GetLength(0); i++)
                for (var j = 0; j < matrix1.GetLength(1); j++)
                    result[i, j] = selector(matrix1, matrix2, i, j);
            return result;
        }

        public static TResult[,] Zip<TSource1, TSource2, TResult>
        (this TSource1[,] matrix1, TSource2[,] matrix2,
            Func<TSource1, TSource2, TResult> selector)
        {
            return matrix1.Zip(matrix2, (m, n, i, j) => selector(m[i, j], n[i, j]));
        }

        public static T Sum<T>(this T[,] matrix)
        {
            var result = default(T);
            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    try
                    {
                        result = (dynamic)result + matrix[i, j];
                    }
                    catch (Exception)
                    {
                        
                        throw new RuntimeBinderException();
                    }
                }
            return result;
        }
    }
}
