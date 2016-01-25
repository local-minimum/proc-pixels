using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{

	public abstract class AbstractPalette : MonoBehaviour {
		
		[SerializeField]
		protected Color[] colors;

		public virtual Color RandomColor {
			get {
				return colors[Random.Range(0, colors.Length)];
			}
		}

		public virtual Color this[int index] {
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