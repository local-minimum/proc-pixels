using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;
using ProcPixel.Utils;

namespace ProcPixel.Artists.Face {
	
	public class NaiveFace : Artist {

		[SerializeField]
		FaceMetaPalette _palette;

		Vector2[] polygon;

		void SetColor() {
			color = _palette[FaceColors.Skin];
		}

		public override void Paint ()
		{			
			SetColor();
			for (int i = 0; i < 6; i++) {
				SetPolygon ();
				Fill ((i < 3 ? ColorShade.Darker : ColorShade.Reference));
			}
			base.Paint ();

		}

		void SetPolygon() {
			polygon = new Vector2[8] {
				new Vector2(Random.Range(.2f, .5f), Random.Range(0f, 0.1f)),
				new Vector2(Random.Range(0f, .3f), Random.Range(.3f, .5f)),
				new Vector2(Random.Range(.1f, .3f), Random.Range(.7f, .8f)),
				new Vector2(Random.Range(.3f, .5f), Random.Range(.8f, .9f)),

				new Vector2(Random.Range(.5f, .7f), Random.Range(.8f, .9f)),
				new Vector2(Random.Range(.7f, .9f), Random.Range(.7f, .8f)),
				new Vector2(Random.Range(.7f, 1f), Random.Range(.3f, .5f)),
				new Vector2(Random.Range(.5f, .8f), Random.Range(0, 0.1f))
			};

			VerifyRestraints ();
			ScaleValues ();
			for (int i=0;i<polygon.Length; i++)
				Debug.Log (polygon [i]);
		}

		void VerifyRestraints() {
			var test = polygon [1] - polygon [0];
			if (test.x > 0)
				polygon [1].x = polygon [0].x;

			test = polygon [7] - polygon [6];
			if (test.x < 0)
				polygon [6].x = polygon [7].x;
		}

		void ScaleValues() {
			Rect extents = rect;
			Debug.Log (extents.width);
			Debug.Log (extents.height);
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
						canvas.Draw (x, y, color [shade]);
				}
			}				
		}
	}
}