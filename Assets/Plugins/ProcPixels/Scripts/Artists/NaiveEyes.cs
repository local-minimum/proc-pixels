using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	public class NaiveEyes : AbstractFace {

		[SerializeField, Range(.05f, .2f)]
		float distance = 0.2f;

		[SerializeField, Range(0.02f, 0.2f)]
		float height = 0.1f;

		[SerializeField, Range(0.1f, 0.2f)]
		float centerUpperElevation = 0.1f;

		[SerializeField, Range(0.01f, 0.1f)]
		float centerLowerDepression = 0.01f;

		[SerializeField, Range(0.2f, 0.5f)]
		float centerAxis = 0.4f;

		[SerializeField, Range(0.2f, 0.5f)]
		float width = 0.4f;

		[SerializeField, Range(0, 4)]
		int smoothings = 0;

		Vector2[] facePolygon;

		protected override void PostProcessing ()
		{
			for (int i=0; i< smoothings;i++)
				drawingLayer = ImageFilters.Erode (drawingLayer, canvasWidth);
		}

		override protected void SetColor() {
			color = _palette [FaceColors.EyeWhite];
		}

		override protected void SetNewValues() {
			distance = Random.Range (.05f, .2f);
			height = Random.Range (.01f, .2f);
			centerUpperElevation = Random.Range (0.1f, 0.2f);
			width = Random.Range (.2f, .5f);
			centerAxis = Random.Range (0.2f, 0.5f);
			centerLowerDepression = Random.Range (0.01f, 0.1f);
		}

		override protected void SetPolygon() {
			if (facePolygon == null)
				throw new System.ArgumentNullException ("Can't draw without a face as template");
			
			Vector2 center = (facePolygon [1] + facePolygon [2] + facePolygon [5] + facePolygon [6]) * 0.25f;
			float xScale = Mathf.Min (facePolygon [5].x - facePolygon [2].x, facePolygon [6].x - facePolygon [1].x);
			float yScale = Mathf.Min (facePolygon [2].y - facePolygon [1].y, facePolygon [6].y - facePolygon [5].y);
			float halfHeight = height / 2f * yScale;
			float distance = this.distance * xScale;
			float widthExtent = distance + xScale * width;
			float centerPosition = distance + xScale * width * centerAxis;
			float centerElevation = yScale * centerUpperElevation;
			float centerDepression = yScale * centerLowerDepression;

			polygon = new Vector2[12] {
				center - new Vector2 (distance, -halfHeight),
				center - new Vector2 (centerPosition, -(halfHeight + centerDepression)),			
				center - new Vector2 (widthExtent, -halfHeight),
				center - new Vector2 (widthExtent, halfHeight),
				center - new Vector2 (centerPosition, halfHeight + centerElevation),			
				center - new Vector2 (distance, halfHeight),

				center + new Vector2 (distance, halfHeight),
				center + new Vector2 (distance, -halfHeight),
				center + new Vector2 (centerPosition, -(halfHeight + centerElevation)),
				center + new Vector2 (widthExtent, -halfHeight),
				center + new Vector2 (widthExtent, halfHeight),
				center + new Vector2 (centerPosition, halfHeight + centerDepression),			
			};

			if (noise > 0)
				ApplyNoise ();
			
		}

		override protected void Fill() {
			Vector2 point;
			int eyeLength = polygon.Length / 2;
			Vector2[] eye = new Vector2[eyeLength];

			for (int i = 0; i < 2; i++) {
				System.Array.Copy (polygon, i * eyeLength, eye, 0, eyeLength);
				var box = PolygonMath.BoundingPixelBox (eye);

				for (int x = Mathf.FloorToInt(box.xMin), X = Mathf.CeilToInt(box.xMax); x < X; x++) {
					for (int y = Mathf.FloorToInt(box.yMin), Y = Mathf.CeilToInt(box.yMax); y < Y; y++) {
						point = new Vector2 (x, y);
						if (PolygonMath.PointInPoly (point, eye)) {
							Draw (x, y, ColorShade.Reference);
						}
					}
				}				
			}

		}

		public override void Paint (Vector2[] parentPolygon)
		{
			facePolygon = parentPolygon;
			Paint ();
		}
	}
}