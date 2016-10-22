using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * ƒл€ борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно примен€ют медианный фильтр, в котором цвет каждого пиксел€, 
		 * замен€етс€ на медиану всех цветов в некоторой окрестности пиксел€.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * »спользуйте окно размером 3х3 дл€ не граничных пикселей,
		 * ќкно размером 2х2 дл€ угловых и 3х2 или 2х3 дл€ граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
		    return original.Select((m, i, j) => m.ApplyFilter(i, j));
		}

	    private static double ApplyFilter(this double[,] original, int x, int y)
	    {
	        var points = new List<double>();
	        const int r = 1; //размер окрестности, если вдруг захочетс€ как-то его измен€ть
            for (var i = -r; i <= r; i++)
                for (var j = -r; j <= r; j++)
                    if (original.HasPoint(x + i, y + j))
                        points.Add(original[x + i, y + j]);
            return points.Median();
	    }

	    private static bool HasPoint(this double[,] original, int x, int y)
	    {
	        return x >= 0 
                && x < original.GetLength(0)
                && y >= 0
                && y < original.GetLength(1);
	    }

	    private static double Median(this IEnumerable<double> input)
	    {
	        var numbers = input.OrderBy(x => x);
	        var count = numbers.Count();
	        var middle = count / 2;
	        return count % 2 == 0
	            ? (numbers.ElementAt(middle) + numbers.ElementAt(middle - 1))/2
	            : numbers.ElementAt(middle);
	    }
	}
}