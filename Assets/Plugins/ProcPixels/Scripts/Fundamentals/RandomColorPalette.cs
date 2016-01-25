using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
	public class RandomColorPalette : AbstractPalette {
		
		public override Color RandomColor {
			get {
				return Color.RandomColor;
			}
		}

		public override Color this[int index] {
			get {
				return Color.RandomColor;
			}
		}

		new int size {
			get {
				return 1;
			}
		}
	}
}