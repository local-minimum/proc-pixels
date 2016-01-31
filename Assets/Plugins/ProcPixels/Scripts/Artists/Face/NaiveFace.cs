using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	
	public class NaiveFace : AbstractFace {

		[SerializeField, Range(0.1f, 0.3f)]
		float chinWidth;

		[SerializeField, Range(0.25f, 0.5f)]
		float cheekWidth;

		[SerializeField, Range(.1f, .4f)]
		float cheekHeight;

		[SerializeField, Range(0.25f, 0.5f)]
		float templeWidth;

		[SerializeField, Range(0.7f, 0.9f)]
		float templeHeight;

		[SerializeField, Range(0.1f, 0.5f)]
		float apexWidth;

		[SerializeField, Range(0.9f, 1f)]
		float apexHeight;

		[SerializeField]
		bool erodeFace = true;	

		Vector2[] parentPolygon;

		override protected void SetColor() {
			color = _palette[FaceColors.Skin];
		}
			
		protected override void PostProcessing ()
		{
			if (erodeFace) {
				//drawingLayer = ImageFilters.Erode (drawingLayer, canvasWidth);
				for (int i=0; i< 3;i++)
					drawingLayer = ImageFilters.Norm (drawingLayer, canvasWidth);
			}
		}

		override protected void SetPolygon() {

			if (randomShape)
				SetNewValues ();


			polygon = new Vector2[8] {
				new Vector2 (0.5f - chinWidth, 0f),
				new Vector2 (0.5f - cheekWidth, cheekHeight),
				new Vector2 (0.5f - templeWidth, templeHeight),
				new Vector2 (0.5f - apexWidth, apexHeight),

				new Vector2 (0.5f + apexWidth, apexHeight),
				new Vector2 (0.5f + templeWidth, templeHeight),
				new Vector2 (0.5f + cheekWidth, cheekHeight),
				new Vector2 (0.5f + chinWidth, 0f)
			};
			if (noise > 0)
				ApplyNoise ();

			ScaleValues ();
		}

		override protected void SetNewValues() {
			chinWidth = Random.Range (0.1f, 0.25f);
			cheekWidth = Random.Range (Mathf.Max(chinWidth * 1.2f, 0.25f), 0.5f);
			templeWidth = Random.Range (cheekWidth * 0.9f, Mathf.Min(cheekWidth * 1.2f, 0.5f));
			apexWidth = Random.Range (0.05f, templeWidth * 0.9f);
			cheekHeight = Random.Range (.1f, .4f);
			templeHeight = Random.Range (0.7f, 0.9f);
			apexHeight = Random.Range (Mathf.Max (templeHeight * 1.2f, 0.8f), 1f);
		}			

		void ScaleValues() {
			float width;
			float height;
			Vector2 offset;

			if (parentPolygon == null) {
				width = rect.width;
				height = rect.height;
				offset = Vector2.zero;
			} else {
				width = parentPolygon [3].x - parentPolygon [0].x;
				height = parentPolygon [1].y - parentPolygon [0].y;
				offset = parentPolygon [0];
			}

			for (int i = 0; i < polygon.Length; i++) {
				polygon [i].x *=  width;
				polygon [i].y *= height;
				polygon [i] +=  offset;			
			}
		}

		protected override void Fill ()
		{
			Fill (ColorShade.Reference);
		}

		void Fill(ColorShade shade) {
			Vector2 point;
			for (int x = 0, X = canvasWidth; x < X; x++) {
				for (int y = 0, Y = canvasHeight; y < Y; y++) {
					point = LineMath.PixelCoordinateToVector(x, y);
					if (PolygonMath.PointInPoly(point, polygon))
						Draw (x, y, shade);
				}
			}				
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			this.parentPolygon = parentPolygon;
			Paint ();
		}
			
	}
}