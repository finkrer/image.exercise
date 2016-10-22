using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * ��� ������ � ���������� �����, �������� ����, ��� �� �����������,
		 * ������ ��������� ��������� ������, � ������� ���� ������� �������, 
		 * ���������� �� ������� ���� ������ � ��������� ����������� �������.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * ����������� ���� �������� 3�3 ��� �� ��������� ��������,
		 * ���� �������� 2�2 ��� ������� � 3�2 ��� 2�3 ��� ���������.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
		    return original.Select((m, i, j) => m.ApplyFilter(i, j));
		}

	    private static double ApplyFilter(this double[,] original, int x, int y)
	    {
	        var points = new List<double>();
	        const int r = 1; //������ �����������, ���� ����� ��������� ���-�� ��� ��������
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