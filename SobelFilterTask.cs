using System;
using System.Linq;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        /* 
		Разберитесь, как работает нижеследующий код (называемый фильтрацией Собеля), 
		и какое отношение к нему имеют эти матрицы:
		
		     | -1 -2 -1 |           | -1  0  1 |
		Sx = |  0  0  0 |      Sy = | -2  0  2 |
		     |  1  2  1 |           | -1  0  1 |
		
		https://ru.wikipedia.org/wiki/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80_%D0%A1%D0%BE%D0%B1%D0%B5%D0%BB%D1%8F
		
		Попробуйте заменить фильтр Собеля 3x3 на фильтр Собеля 5x5 и сравните результаты. 
		http://www.cim.mcgill.ca/~image529/TA529/Image529_99/assignments/edge_detection/references/sobel.htm

		Обобщите код применения фильтра так, чтобы можно было передавать ему любые матрицы, любого нечетного размера.
		Фильтры Собеля размеров 3 и 5 должны быть частным случаем. 
		После такого обобщения менять фильтр Собеля одного размера на другой будет легко.
		*/

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var sy = sx.Transpose();
            var filterSize = sx.GetLength(0);
            var filterRadius = (filterSize - 1)/2;
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];

            for (var x = filterRadius; x < width - filterRadius; x++)
                for (var y = filterRadius; y < height - filterRadius; y++)
                {
                    var n = g.GetNeighborhood(x, y, filterSize);
                    var gx = n.Convolute(sx);
                    var gy = n.Convolute(sy);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result.Normalize();
        }

        private static double[,] Normalize(this double[,] matrix)
        {
            var min = matrix.ToIEnumerable().Min();
            var max = matrix.ToIEnumerable().Max();
            return matrix.Select(x => (x - min)/(max - min));
        }

        private static double[,] Transpose(this double[,] matrix)
        {
            return matrix.Select((m, i, j) => m[j, i]);
        }

        private static double Convolute(this double[,] matrix, double[,] kernel)
        {
            return matrix
                .Zip(kernel, (x, y) => x*y)
                .ToIEnumerable()
                .Aggregate((x, y) => x + y);
        }

        private static double[,] GetNeighborhood(this double[,] matrix, int x, int y, int size)
        {
            var r = (size - 1)/2;
            return matrix.TakeBlock(x - r, x + r, y - r, y + r);
        }
    }
}