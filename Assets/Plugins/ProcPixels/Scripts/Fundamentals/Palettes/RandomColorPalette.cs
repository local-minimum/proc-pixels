using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
	public class RandomColorPalette : AbstractPalette {

		[SerializeField]
		int _size = 1;

		public override ShadedColor RandomColor {
			get {
				return ShadedColor.RandomColor;
			}
		}

		public override ShadedColor this[int index] {
			get {
				return ShadedColor.RandomColor;
			}
		}

		public override int size {
			get {
				return _size;
			}
		}
	}
}