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

		protected override void _Paint ()
		{

			if (remakePaletteOnPaint)
				_palette.SetRandomColorsFromPalettes ();

			SetColor();
			ClearDrawingLayer ();

			if (randomShape)
				SetNewValues ();

			SetPolygon ();
			Fill ();
			PostProcessing ();

		}
			
	}
}