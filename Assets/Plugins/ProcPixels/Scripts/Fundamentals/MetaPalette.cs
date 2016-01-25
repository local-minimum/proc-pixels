using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {
	public class MetaPalette : AbstractPalette {

		[SerializeField]
		AbstractPalette[] palettes;

		new int size {
			get {
				int s = 0;
				for (int i = 0; i < palettes.Length; i++) {
					s += palettes [i].size;
				}
				return s;
			}
		}

		public override Color this[int index] {
			get {
				int pos = index;
				for (int i = 0; i < palettes.Length; i++) {
					if (pos < palettes [i].size) {
						return palettes [i] [pos];
					} else {
						pos -= palettes [i].size;
					}
				}
				throw new System.ArgumentException(string.Format("Index {0} out of range. Size is {1} ({2} palettes)", index, size, palettes.Length));
					
			}
			set {
				int pos = index;
				for (int i = 0; i < palettes.Length; i++) {
					if (pos < palettes [i].size) {
						palettes [i] [pos] = value;
						return;
					} else {
						pos -= palettes [i].size;
					}
				}
				throw new System.ArgumentException(string.Format("Index {0} out of range. Size is {1} ({2} palettes)", index, size, palettes.Length));

			}
		}

		public override Color RandomColor {
			get {
				return this [Random.Range (0, size)];
			}
		}
	}
}