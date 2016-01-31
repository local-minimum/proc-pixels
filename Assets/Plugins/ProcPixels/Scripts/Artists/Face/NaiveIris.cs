using UnityEngine;
using System.Collections;
using ProcPixel.Utils;
using ProcPixel.Palettes;
using ProcPixel.Fundamentals;

namespace ProcPixel.Artists.Face {
	public class NaiveIris : AbstractFace {

		Vector2[] eyeShape;

		[SerializeField, Range(0.4f, 1f)] 
		float maxEyeHalfWidthIris = 0.5f;

		[SerializeField, Range(0.5f, 1.5f)]
		float sougthAspectRatio = 1f;

		protected override void SetNewValues ()
		{
			maxEyeHalfWidthIris = Random.Range (0.4f, 1f);
			sougthAspectRatio = Random.Range (0.5f, 1.5f);
		}

		protected override void SetColor ()
		{
			color = _palette [FaceColors.EyeIris];
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			eyeShape = parentPolygon;
			base.Paint ();
		} 

		protected override void PostProcessing ()
		{
			
		}

		protected override void SetPolygon ()
		{
			if (eyeShape == null)
				throw new System.ArgumentNullException ("The irises needs its eyes");

			Vector2 centerLeft = (eyeShape [1] + eyeShape [4]) * 0.5f;
			float widthLeft = Mathf.Min (Mathf.Min (eyeShape [0].x - eyeShape [2].x, eyeShape [5].x - eyeShape [3].x) * maxEyeHalfWidthIris,
				                  (eyeShape [4].y - eyeShape [1].y) * sougthAspectRatio);
			
			Vector2 centerRight = (eyeShape [11] + eyeShape [8]) * 0.5f;
			float widthRight = Mathf.Min (Mathf.Min (eyeShape [10].x - eyeShape [6].x, eyeShape[9].x - eyeShape [7].x) * maxEyeHalfWidthIris,
				(eyeShape [8].y - eyeShape [11].y) * sougthAspectRatio);

			polygon = new Vector2[8] {
				eyeShape [1],
				centerLeft + Vector2.left * widthLeft / 2f,
				eyeShape [4],
				centerLeft + Vector2.right * widthLeft / 2f,

				eyeShape [11],
				centerRight + Vector2.left * widthRight /2f,
				eyeShape [8],
				centerRight + Vector2.right * widthRight /2f
			};

			if (noise > 0)
				ApplyNoise ();
				
		}

		protected override void Fill ()
		{
			Vector2 point;
			int eyeLength = polygon.Length / 2;
			Vector2[] eye = new Vector2[eyeLength];

			for (int i = 0; i < 2; i++) {
				System.Array.Copy (polygon, i * eyeLength, eye, 0, eyeLength);
				var box = PolygonMath.BoundingPixelBox (eye);

				for (int x = Mathf.FloorToInt (box.xMin), X = Mathf.CeilToInt (box.xMax); x < X; x++) {
					for (int y = Mathf.FloorToInt (box.yMin), Y = Mathf.CeilToInt (box.yMax); y < Y; y++) {
						point = LineMath.PixelCoordinateToVector(x, y);
						if (PolygonMath.PointInPoly (point, eye)) {
							Draw (x, y, ProcPixel.Fundamentals.ShadedColor.RandomShade);
						}
					}
				}
			}
		}
	}
}