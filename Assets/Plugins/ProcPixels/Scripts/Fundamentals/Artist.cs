using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {
	public class Artist : MonoBehaviour {

		PaintCanvas _canvas;

		[SerializeField]
		Artist[] subArtist;

		int canvasWidth;

		public PaintCanvas canvas {
			get {
				return _canvas;
			}

			set {
				if (_canvas != null)
					_canvas.OnNewCanvas -= HandleNewCanvas;

				_canvas = value;
				_canvas.OnNewCanvas += HandleNewCanvas;

				HandleNewCanvas (_canvas.currentSprite);

				for (int i = 0; i < subArtist.Length; i++)
					subArtist [i].canvas = value;
				
			}
		}

		void HandleNewCanvas(Sprite sprite) {
			canvasWidth = Mathf.RoundToInt(sprite.rect.width);
		}
	
	}
}