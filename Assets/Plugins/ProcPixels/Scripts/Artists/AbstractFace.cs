using UnityEngine;
using System.Collections;
using ProcPixel.Fundamentals;
using ProcPixel.Palettes;

namespace ProcPixel.Artists.Face {
	public abstract class AbstractFace : Artist {
		
		[SerializeField]
		protected FaceMetaPalette _palette;

		[SerializeField, Range(0f, 0.5f)]
		protected float noise;

		[SerializeField]
		protected bool randomShape = true;

		[SerializeField]
		protected bool clearBeforePaint = false;

		[SerializeField]
		protected bool remakePaletteOnPaint = false;

		protected void ApplyNoise() {
			for (int i = 0; i < polygon.Length; i++)
				polygon [i] += new Vector2 (Random.Range (-noise, noise), Random.Range (-noise, noise));
		}

		abstract protected void SetColor ();

		abstract protected void SetNewValues ();

		abstract protected void SetPolygon ();

		abstract protected void Fill();

		abstract protected void PostProcessing ();

		public override void Paint ()
		{
			if (clearBeforePaint)
				canvas.Clear ();

			if (remakePaletteOnPaint)
				_palette.SetRandomColorsFromPalettes ();

			SetColor();
			ClearDrawingLayer ();

			if (randomShape)
				SetNewValues ();

			SetPolygon ();
			Fill ();
			PostProcessing ();

			base.Paint ();
		}

		#if UNITY_EDITOR
		void OnDrawGizmosSelected() {
			if (polygon == null)
				return;
			
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