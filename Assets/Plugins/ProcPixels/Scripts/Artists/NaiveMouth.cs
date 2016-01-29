using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;

namespace ProcPixel.Artists.Face {
	public class NaiveMouth : AbstractFace {

		public override void Paint ()
		{
			base.Paint ();
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			base.Paint (parentPolygon);
		}

		void SetColor () {
			color = _palette [FaceColors.Mouth];
		}
	}
}