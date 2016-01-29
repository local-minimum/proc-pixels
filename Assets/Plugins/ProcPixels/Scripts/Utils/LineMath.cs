using UnityEngine;
using System.Collections.Generic;

namespace ProcPixel.Utils {

	public enum AdjancanyCondition {Cross, Compass, Line};

	public static class LineMath {

		public static int[] VectorLineToPixels(int canvasWidth, int canvasHeight, AdjancanyCondition adjacancy, params Vector2[] points) {

			if (adjacancy != AdjancanyCondition.Line)
				return VectorLineToPixelsViaSnaking (canvasWidth, canvasHeight, adjacancy, points);
							
			List<int> pixels = new List<int>();
			int currentPos = PointToPixelPosition(points[0], canvasWidth, canvasHeight);
			pixels.Add(currentPos);
			float stepSize = 0.5f;

			for (int i = 1; i < points.Length; i++) {
				Vector2 source = points [i - 1];
				Vector2 target = points [i];
				float lerpStep = 1f / (Vector2.Distance (source, target) / stepSize);
				for (float t=0; t<=1f; t+=lerpStep) {
					int nextPos = PointToPixelPosition (Vector2.Lerp (source, target, t), canvasWidth, canvasHeight);
					if (nextPos != currentPos) {
						pixels.Add (nextPos);
						currentPos = nextPos;
					}
				}
			}
			return pixels.ToArray ();
		}

		static int[] VectorLineToPixelsViaSnaking(int canvasWidth, int canvasHeight, AdjancanyCondition adjacancy, params Vector2[] points) {

			var offsets = GetOffsets (adjacancy, canvasWidth);
			List<int> pixels = new List<int>();
			int target;
			int currentPos = PointToPixelPosition(points[0], canvasWidth, canvasHeight);
			pixels.Add(currentPos);
			int nextPos;

			for (int i = 1; i < points.Length; i++) {
				target = PointToPixelPosition(points [i], canvasWidth, canvasHeight);
				while (CloserNeighbour(currentPos, target, offsets, canvasWidth, canvasHeight, out nextPos)) {
					pixels.Add (nextPos);
					currentPos = nextPos;
				}
			}
			return pixels.ToArray ();
		}


		public static int PointToPixelPosition(Vector2 point, int canvasWidth, int canvasHeight) {
			int x = Mathf.Clamp (Mathf.RoundToInt (point.x), 0, canvasWidth - 1);
			int y = Mathf.Clamp (Mathf.RoundToInt (point.y), 0, canvasHeight - 1);
			return y * canvasWidth + x;
		}

		static int[] GetOffsets(AdjancanyCondition adjacancy, int canvasWidth) {
			int[] offsets;

			if (adjacancy == AdjancanyCondition.Compass)
				offsets = new int[8];
			else
				offsets = new int[4];

			offsets [0] = -1;
			offsets [1] = 1;
			offsets [2] = -canvasWidth;
			offsets [3] = canvasWidth;

			if (adjacancy == AdjancanyCondition.Compass) {
				offsets [4] = -canvasWidth - 1;
				offsets [5] = -canvasWidth + 1;
				offsets [6] = canvasWidth + 1;
				offsets [7] = canvasWidth - 1;
			}
			return offsets;
		}

		static bool CloserNeighbour(int currentPosition, int target, int[] offsets, int canvasWidth, int canvasHeight, out int neighbour) {

			if (currentPosition == target) {
				neighbour = currentPosition;
				return false;
			}
			for (int i = 0; i < offsets.Length; i++) {
				
			}
			neighbour = currentPosition;
			return false;
		}

		public static float EuclideanDistance(int A, int B, int canvasWidth) {
			throw new System.NotImplementedException ();
		}
	}
}