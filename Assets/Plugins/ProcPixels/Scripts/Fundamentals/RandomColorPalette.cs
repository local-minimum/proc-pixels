using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
	public class RandomColorPalette : AbstractPalette {

		[SerializeField]
		int _size = 1;

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
				return _size;
			}
		}
	}
}