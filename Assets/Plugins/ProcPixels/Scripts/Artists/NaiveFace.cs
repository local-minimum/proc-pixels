using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	
	public class NaiveFace : Artist {

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

		[SerializeField, Range(0f, 0.1f)]
		float noise;

		[SerializeField]
		FaceMetaPalette _palette;

		[SerializeField]
		bool randomShape = true;

		[SerializeField]
		bool clearBeforePaint = true;

		[SerializeField]
		bool remakePaletteOnPaint = true;

		[SerializeField]
		bool erodeFace = true;

		Vector2[] polygon;

		void SetColor() {
			color = _palette[FaceColors.Skin];
		}

		public override void Paint ()
		{			
			if (clearBeforePaint)
				canvas.Clear ();
			
			if (remakePaletteOnPaint)
				_palette.SetRandomColorsFromPalettes ();
			
			SetColor();
			ClearDrawingLayer ();

			SetPolygon ();
			Fill (ColorShade.Reference); 
			if (erodeFace) {
				//drawingLayer = ImageFilters.Erode (drawingLayer, canvasWidth);
				for (int i=0; i< 3;i++)
					drawingLayer = ImageFilters.Norm (drawingLayer, canvasWidth);
			}
			base.Paint ();

		}

		void SetPolygon() {

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
				SetNoise ();

			ScaleValues ();
		}

		void SetNewValues() {
			chinWidth = Random.Range (0.1f, 0.25f);
			cheekWidth = Random.Range (Mathf.Max(chinWidth * 1.2f, 0.25f), 0.5f);
			templeWidth = Random.Range (cheekWidth * 0.9f, Mathf.Min(cheekWidth * 1.2f, 0.5f));
			apexWidth = Random.Range (0.05f, templeWidth * 0.9f);
			cheekHeight = Random.Range (.1f, .4f);
			templeHeight = Random.Range (0.7f, 0.9f);
			apexHeight = Random.Range (Mathf.Max (templeHeight * 1.2f, 0.8f), 1f);
		}

		void SetNoise() {
			for (int i = 0; i < polygon.Length; i++)
				polygon [i] += new Vector2 (Random.Range (-noise, noise), Random.Range (-noise, noise));
		}

		void ScaleValues() {
			Rect extents = rect;
			for (int i = 0; i < polygon.Length; i++) {
				polygon [i].x *=  extents.width;
				polygon [i].y *= extents.height;
			}
		}

		void Fill(ColorShade shade) {
			Vector2 point;
			for (int x = 0, X = canvasWidth; x < X; x++) {
				for (int y = 0, Y = canvasHeight; y < Y; y++) {
					point = new Vector2 (x, y);
					if (PolygoneMath.PointInPoly(point, polygon))
						Draw (x, y, shade);
				}
			}				
		}

		#if UNITY_EDITOR
		void OnDrawGizmosSelected() {
			float size = 4f;
			for (int i = 0; i < polygon.Length; i++) {
				int j = (i + 1) % polygon.Length;
				Gizmos.DrawLine (transform.TransformPoint (polygon [i] * size ), transform.TransformPoint (polygon [j] * size));
				Gizmos.DrawSphere(transform.TransformPoint(polygon[i] * size), 3f * size);

			}
		}

		#endif
	}
}