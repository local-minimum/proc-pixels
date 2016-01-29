using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;

namespace ProcPixel.Artists.Face {
	public class NaiveMouth : AbstractFace {

		Vector2[] facePolygon;

		public override void Paint (Vector2[] parentPolygon)
		{
			facePolygon = parentPolygon;

			base.Paint ();
		}

		protected override void PostProcessing ()
		{

		}

		protected override void SetColor () {
			color = _palette [FaceColors.Mouth];
		}

		protected override void Fill ()
		{
			throw new System.NotImplementedException ();
		}

		protected override void SetPolygon ()
		{	
			if (facePolygon == null)
				throw new System.ArgumentNullException ("Can't paint mouth without face");

			if (noise > 0)
				ApplyNoise ();
		}

		protected override void SetNewValues ()
		{
			throw new System.NotImplementedException ();
		}
	}
}