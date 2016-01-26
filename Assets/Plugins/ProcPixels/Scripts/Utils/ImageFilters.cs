using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;

namespace ProcPixel.Utils {
	public static class ImageFilters {

		static int[] GetKernel(int width) {
			return new int[9] {
				-width - 1,
				-width,
				-width + 1,
				-1,
				0,
				1,
				width - 1,
				width,
				width + 1
			};
		}

		public static ColorShade[] Erode(ColorShade[] data, int width) {			
			ColorShade[] ret = new ColorShade[data.Length];
			System.Array.Copy (data, ret, data.Length);

			var offsets = GetKernel (width);
			int kernelSize = offsets.Length;

			for (int pos = 0; pos < data.Length; pos++) {
				for (int i = 0; i < kernelSize; i++) {
					int testPos = pos + offsets[i];
					if (testPos < 0 || testPos > data.Length || data[testPos] == ColorShade.None) {
						ret [pos] = ColorShade.None;
						break;
					}
				}
			}

			return ret;
		}

		public static ColorShade[] Norm(ColorShade[] data, int width) {
			ColorShade[] ret = new ColorShade[data.Length];
			System.Array.Copy (data, ret, data.Length);

			var offsets = GetKernel (width);
			int kernelSize = offsets.Length;

			for (int pos = 0; pos < data.Length; pos++) {
				var counts = new int[4] {0, 0, 0, 0};
				for (int i = 0; i < kernelSize; i++) {
					int testPos = pos + offsets [i];
					if (testPos < 0 || testPos >= data.Length)
						counts [0]++;
					else {
						//Debug.Log(testPos + " " + data.Length);
						counts [System.Convert.ToInt32 (data [testPos])]++;
					}
				}
				int max = 0;
				ColorShade color = ColorShade.None;
				for (int i = 0; i < 4; i++) {
					if (counts [i] > max) {
						max = counts [i];
						color = (ColorShade)i;
					}
				}
				ret [pos] = color;
			}

			return ret;
		}
		
	}
}