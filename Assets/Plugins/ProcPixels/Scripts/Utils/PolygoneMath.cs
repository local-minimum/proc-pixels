using UnityEngine;
using System.Collections;

namespace ProcPixel.Utils {
	
	public static class PolygoneMath {

		public static bool PointInPoly(Vector2 point, Vector2[] edges) {
			Vector2[] transposed = new Vector2[edges.Length];
			for (int i = 0; i < edges.Length; i++) {
				transposed [i] = edges [i] - point;
			}
			for (int i = 0; i < edges.Length; i++) {
				int j = (i + 1) % edges.Length;
				if ((transposed [j].x * transposed [i].y -
					transposed [i].x * transposed [j].y) <= 0f)
					return false;
			}
			return true;
		}
	}
}