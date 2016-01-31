using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
	public class EnumeratedMetaPalette<T> : EnumeratedPalette<T> where T : System.IConvertible
	{
		[SerializeField]
		AbstractPalette[] palettes;

		[SerializeField]
		bool enforceShading = false;

		[SerializeField]
		float shading = 0f;

		new void Reset ()
		{
			var l = EnumLength;
			colors = new ShadedColor[l];
			palettes = new AbstractPalette[l];
		}

		public void SetRandomColorsFromPalettes() {
			
			for (int i = 0; i < palettes.Length; i++) {
				var color = palettes [i].RandomColor.Copy();
				if (enforceShading)
					color.SetShadeStrength (shading);
				colors [i] = color;
			}
		}
	}
}
