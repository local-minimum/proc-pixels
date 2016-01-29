﻿using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	public class NaiveEyes : AbstractFace {

		[SerializeField, Range(.05f, .2f)]
		float distance = 0.2f;

		[SerializeField, Range(0.02f, 0.3f)]
		float height = 0.1f;

		[SerializeField, Range(0.2f, 0.5f)]
		float width = 0.4f;

		[SerializeField, Range(0, 4)]
		int smoothings = 0;

		Vector2[] facePolygon;

		public override void Paint ()
		{
			if (facePolygon == null)
				return;

			SetColor();
			ClearDrawingLayer ();

			if (randomShape)
				SetNewValues ();
			
			SetPolygon ();
			Fill ();
			for (int i=0; i< smoothings;i++)
				drawingLayer = ImageFilters.Erode (drawingLayer, canvasWidth);
			

			base.Paint ();
		}

		void SetColor() {
			color = _palette [FaceColors.Eye];
		}

		void SetNewValues() {
			distance = Random.Range (.05f, .2f);
			height = Random.Range (.1f, .3f);
			width = Random.Range (.2f, .5f);
		}

		void SetPolygon() {
			Vector2 center = (facePolygon [1] + facePolygon [2] + facePolygon [5] + facePolygon [6]) * 0.25f;
			float xScale = Mathf.Min (facePolygon [5].x - facePolygon [2].x, facePolygon [6].x - facePolygon [1].x);
			float yScale = Mathf.Min (facePolygon [2].y - facePolygon [1].y, facePolygon [6].y - facePolygon [5].y);
			float halfHeight = height / 2f * yScale;
			float distance = this.distance * xScale;
			float widthExtent = distance + xScale * width;

			polygon = new Vector2[8] {
				center - new Vector2 (distance, -halfHeight),
				center - new Vector2 (widthExtent, -halfHeight),
				center - new Vector2 (widthExtent, halfHeight),
				center - new Vector2 (distance, halfHeight),

				center + new Vector2 (distance, halfHeight),
				center + new Vector2 (distance, -halfHeight),
				center + new Vector2 (widthExtent, -halfHeight),
				center + new Vector2 (widthExtent, halfHeight)
			};

			if (noise > 0)
				SetNoise ();
			
		}

		void Fill() {
			Vector2 point;
			int eyeLength = polygon.Length / 2;
			Vector2[] eye = new Vector2[eyeLength];
			int count = 0;
			for (int i = 0; i < 2; i++) {
				System.Array.Copy (polygon, i * eyeLength, eye, 0, eyeLength);
				var box = PolygoneMath.BoundingPixelBox (eye);

				for (int x = Mathf.FloorToInt(box.xMin), X = Mathf.CeilToInt(box.xMax); x < X; x++) {
					for (int y = Mathf.FloorToInt(box.yMin), Y = Mathf.CeilToInt(box.yMax); y < Y; y++) {
						point = new Vector2 (x, y);
						if (PolygoneMath.PointInPoly (point, eye)) {
							Draw (x, y, ColorShade.Reference);
							count++;
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