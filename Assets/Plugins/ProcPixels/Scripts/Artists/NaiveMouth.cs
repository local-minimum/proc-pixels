using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	public class NaiveMouth : AbstractFace {

		Vector2[] facePolygon;

		[SerializeField, Range(0.3f, 0.7f)]
		float mouthWidth = 0.4f;

		[SerializeField, Range(0.05f, .4f)]
		float mouthUpperPeaksDistance = 0.1f;

		[SerializeField, Range(0.01f, 0.1f)]
		float mouthUpperPeaksElevation = 0.05f;

		[SerializeField, Range(0.1f, 0.2f)]
		float mouthUpperHeight = 0.1f;

		[SerializeField, Range(-.4f, 0.4f)]
		float smile = 0f;

		[SerializeField, Range(.1f, .5f)]
		float lowerHeight = 0.2f;

		protected override void SetNewValues ()
		{
			mouthWidth = Random.Range (0.3f, .7f);
			mouthUpperPeaksDistance = Random.Range (0.05f, .4f);
			mouthUpperPeaksElevation = Random.Range(0.01f, 0.2f);
			mouthUpperHeight = Random.Range (0.1f, 0.2f);
			smile = Random.Range (-.4f, 0.4f);
			lowerHeight = Random.Range (.1f, .5f);
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			facePolygon = parentPolygon;
			base.Paint ();
		}

		protected override void PostProcessing ()
		{
			drawingLayer = ImageFilters.NormExpand (drawingLayer, canvasWidth);
		}

		protected override void SetColor () {
			color = _palette [FaceColors.Mouth];
		}

		protected override void Fill ()
		{
			Rect box = PolygonMath.BoundingPixelBox (polygon);
			Vector2 point;
			int triangle;

			for (int x = Mathf.FloorToInt (box.xMin), X = Mathf.CeilToInt (box.xMax); x < X; x++) {
				for (int y = Mathf.FloorToInt (box.yMin), Y = Mathf.CeilToInt (box.yMax); y < Y; y++) {
					point = new Vector2 (x, y);
					if (PolygonMath.PointInTriangleSequence(point, polygon, out triangle)) {
						Draw (x, y, (triangle == 0 || triangle == 5) ? ColorShade.Reference : ColorShade.Lighter);
					}
				}
			}
		}

		protected override void SetPolygon ()
		{	
			if (facePolygon == null)
				throw new System.ArgumentNullException ("Can't paint mouth without face");

			float xScale = facePolygon [6].x - facePolygon [1].x;
			float yScale = ((facePolygon [1].y - facePolygon [0].y) + (facePolygon [6].y - facePolygon [7].y)) * 0.5f;
			Vector2 center = (facePolygon [1] + facePolygon [6]) * 0.5f + Vector2.down * yScale * 0.1f;
			float smileHeight = yScale * (lowerHeight + mouthUpperHeight) * smile;
			float scaledHalftWidth = xScale * mouthWidth * 0.5f;
			float scaledApexHeight = yScale * (mouthUpperPeaksElevation + mouthUpperHeight);
			float scaledApexPosition = scaledHalftWidth * mouthUpperPeaksDistance * 0.5f;

				
			polygon = new Vector2[18] {
				
				center + Vector2.down * yScale * lowerHeight,
				center + Vector2.up * smileHeight + Vector2.left * scaledHalftWidth,
				center,

				center,
				center + Vector2.up * smileHeight + Vector2.left * scaledHalftWidth,
				center + Vector2.up * scaledApexHeight + Vector2.left * scaledApexPosition,

				center,
				center + Vector2.up * scaledApexHeight + Vector2.left * scaledApexPosition,
				center + Vector2.up * yScale * mouthUpperHeight,

				center,
				center + Vector2.up * yScale * mouthUpperHeight,
				center + Vector2.up * scaledApexHeight + Vector2.right * scaledApexPosition,

				center,
				center + Vector2.up * scaledApexHeight + Vector2.right * scaledApexPosition,
				center + Vector2.up * smileHeight + Vector2.right * scaledHalftWidth,

				center,
				center + Vector2.up * smileHeight + Vector2.right * scaledHalftWidth,
				center + Vector2.down * yScale * lowerHeight,

			};

			if (noise > 0)
				ApplyNoise ();
		}
	}
}