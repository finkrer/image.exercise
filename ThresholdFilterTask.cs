using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		/* 
		 * Замените пиксели ярче порогового значения T на белый (1.0),
		 * а остальные на черный (0.0).
		 * Пороговое значение найдите так, чтобы:
		 *  - если N — общее количество пикселей изображения, 
		 *    то хотя бы (int)(threshold*N)  пикселей стали белыми;
		 *  - белыми стало как можно меньше пикселей.
		*/

		public static double[,] ThresholdFilter(double[,] original, double threshold)
		{
		    var t = FindT(original, threshold);
		    return original.Select(x => x >= t ? 1d : 0);
        }

	    public static double FindT(double[,] original, double threshold)
	    {
	        var target = (int)Math.Ceiling(original.Cast<double>().Count() * threshold);
	        var t = 0d;
	        while (CheckT(original, t) > target)
	            t += 0.01;
	        return t;
	    }

	    private static double CheckT(double[,] original, double t)
	    {
	        return original.Cast<double>().Count(x => x > t);
	    }
    }
}