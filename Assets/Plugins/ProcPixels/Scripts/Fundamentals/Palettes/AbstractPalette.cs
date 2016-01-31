using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{

	public abstract class AbstractPalette : MonoBehaviour {
		
		[SerializeField]
		protected ShadedColor[] colors;

		public virtual ShadedColor RandomColor {
			get {
				return colors[Random.Range(0, colors.Length)];
			}
		}

		public virtual ShadedColor this[int index] {
			get {
				return colors [index];
			}

			set {
				colors [index] = value;
			}
		}

		public virtual int size {
			get {
				return colors.Length;
			}
		}
	}
}