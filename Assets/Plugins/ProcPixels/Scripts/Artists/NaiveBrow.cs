using UnityEngine;
using System.Collections;
using ProcPixel.Palettes;
using ProcPixel.Utils;
using ProcPixel.Fundamentals;

namespace ProcPixel.Artists.Face {
	public class NaiveBrow : AbstractFace {

		Vector2[] parentEye;

		[SerializeField, Range(0.2f, 0.5f)]
		float verticalDistance = 0;

		[SerializeField, Range(0, 0.5f)]
		float centerAccent = 0;

		[SerializeField, Range(-.1f, .3f)]
		float centerXOffset = 0;

		[SerializeField]
		bool smoothGrow = false;

		protected override void SetNewValues ()
		{
			verticalDistance = Random.Range (0.2f, 0.5f);
			centerAccent = Random.Range (0, 0.5f);
			centerXOffset = Random.Range (-.1f, .3f);
			smoothGrow = Random.value <= 0.5f;
		}

		protected override void SetColor ()
		{
			color = _palette [FaceColors.Hair];
		}


		protected override void PostProcessing ()
		{
			if (smoothGrow)
				drawingLayer = ImageFilters.NormExpand (drawingLayer, canvasWidth);

		}

		protected override void SetPolygon ()
		{
			if (parentEye == null)
				throw new System.ArgumentNullException ("No eye no brow!");

			float eyeHeightLeft = ((parentEye [3].y - parentEye [2].y) + (parentEye [4].y - parentEye [1].y) + (parentEye [5].y - parentEye [4].y)) / 3f;
			float eyeHeightRight = ((parentEye [7].y - parentEye [6].y) + (parentEye [8].y - parentEye [11].y) + (parentEye [9].y - parentEye [10].y)) / 3f;

			float eyeWidthLeft = parentEye [5].x - parentEye [3].x;
			float eyeWidthRight = parentEye [9].x - parentEye [7].x;

			polygon = new Vector2[6] {
				parentEye[3] + Vector2.up * eyeHeightLeft * verticalDistance,
				parentEye[4]  + Vector2.right * eyeWidthLeft * centerXOffset + Vector2.up * eyeHeightLeft * (verticalDistance + centerAccent),
				parentEye[5] + Vector2.up * eyeHeightLeft * verticalDistance,

				parentEye[7] + Vector2.up * eyeHeightRight * verticalDistance,
				parentEye[8] + Vector2.up * eyeHeightRight * (verticalDistance + centerAccent) + Vector2.left * eyeWidthRight * centerXOffset,
				parentEye[9] + Vector2.up * eyeHeightRight * verticalDistance

			};

			if (noise > 0)
				ApplyNoise ();
			
		}

		protected override void Fill ()
		{
			for (int offset = 0; offset < polygon.Length; offset += 3) {
				var line = LineMath.VectorLineToPixels (canvasWidth, canvasHeight, AdjancanyCondition.Line, polygon [offset], polygon [offset + 1], polygon [offset + 2]);
				for (int i = 0; i < line.Length; i++) {
					Draw (line [i], ColorShade.Reference);
				}	

			}
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			parentEye = parentPolygon;
			Paint ();
		}


	}
}