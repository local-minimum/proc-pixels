using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
	public class EnumeratedMetaPalette<T> : EnumeratedPalette<T> where T : System.IConvertible
	{
		[SerializeField]
		AbstractPalette[] palettes;

		[SerializeField]
		float shading;

		new void Reset ()
		{
			var l = EnumLength;
			colors = new Color[l];
			palettes = new AbstractPalette[l];
		}

		public void SetRandomColorsFromPalettes() {
			bool setShading = shading >= 0f;
			for (int i = 0; i < palettes.Length; i++) {
				var color = palettes [i].RandomColor;
				if (setShading)
					color.SetShadeStrength (shading);
				colors [i] = color;
			}
		}
	}
}
