using System;
using System.Drawing;

namespace Recognizer
{
	public class Pixel
	{
		public byte R { get; }
		public byte G { get; }
		public byte B { get; }
	
		public Pixel(byte r, byte g, byte b)
		{
			R = r;
			G = g;
			B = b;
		}

		public Pixel(Color color)
		{
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public override string ToString()
		{
			return $"Pixel({R}, {G}, {B})";
		}
	}
}