using UnityEngine;
using System.Collections;

namespace ProcPixel.Utils {
	
	public static class PolygonMath {

		public static bool PointInTriangleSequence(Vector2 point, Vector2[] tris, out int triangle) {
			if (tris.Length % 3 != 0)
				throw new System.ArgumentException ("Triangles should be a multiple of 3 on last count " + tris.Length + " is not!");

			Vector2[] transposed = TransposePolygon (point, tris);
			for (int i = 0; i < transposed.Length; i += 3) {
				if (IsCCW (transposed, i, i + 3)) {
					triangle = i / 3;
					return true;
				}
			}
			triangle = -1;
			return false;
		}

		public static bool PointInPoly(Vector2 point, Vector2[] edges) {
			Vector2[] transposed = TransposePolygon (point, edges);
			return IsCCW (transposed);
		}

		static Vector2[] TransposePolygon(Vector2 point, Vector2[] edges) {
			return TransposePolygon (point, edges, 0, edges.Length);
		}
			
		static Vector2[] TransposePolygon(Vector2 point, Vector2[] edges, int startIndex, int length) {
			Vector2[] transposed = new Vector2[length];
			length += startIndex;
			for (int i = startIndex; i < length; i++) {
				transposed [i - startIndex] = edges [i] - point;
			}
			return transposed;
		}

		static bool IsCCW(Vector2[] edges) {
			return IsCCW(edges, 0, edges.Length);
		}

		/// <summary>
		/// Test if the polygon is counter clock-wise
		/// </summary>
		/// <returns><c>true</c>, if triangle was CCW, <c>false</c> otherwise.</returns>
		/// <param name="edges">Edges.</param>
		/// <param name="startIndex">Start index, inclusive</param>
		/// <param name="endIndex">End, exclusive</param>
		static bool IsCCW(Vector2[] edges, int startIndex, int endIndex) {
			for (int i = startIndex; i < endIndex; i++) {
				int j = ((i + 1) % (endIndex - startIndex)) + startIndex;
				if ((edges [j].x * edges [i].y -
					edges [i].x * edges [j].y) < 0f)
					return false;
			}
			return true;

		}

		public static Rect BoundingPixelBox(Vector2[] polygon) {
			var v = polygon [0];
			float x0 = v.x;
			float x1 = v.x;
			float y0 = v.y;
			float y1 = v.y;

			for (int i = 1; i < polygon.Length; i++) {
				v = polygon [i];

				if (v.x < x0)
					x0 = v.x;
				else if (v.x > x1)
					x1 = v.x;
				
				if (v.y < y0)
					y0 = v.y;
				else if (v.y > y1)
					y1 = v.y;
			}

			return new Rect (x0, y0, x1 - x0 + 1, y1 - y0 + 1);
		}
	}
}