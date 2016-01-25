using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {
	public class Artist : MonoBehaviour {

		PaintCanvas _canvas;

		[SerializeField]
		private Artist[] subArtist;

		ProcPixel.Fundamentals.Color _color;

		public ProcPixel.Fundamentals.Color color {
			get {
				return _color;
			}

			set {
				_color = value;
				for (int i = 0; i < subArtist.Length; i++)
					subArtist [i].color = value;
			}
		}

		int _canvasWidth;
		int _canvasHeight;

		public int canvasWidth {
			get {
				return _canvasWidth;
			}
		}

		public int canvasHeight {
			get {
				return _canvasHeight;
			}
		}

		Rect _rect;
		public Rect rect {
			get {
				return new Rect (_rect);
			}
		}
	
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
			_rect = sprite.rect;
			_canvasWidth = Mathf.RoundToInt(sprite.rect.width);
			_canvasHeight = Mathf.RoundToInt (sprite.rect.height);
		}
	
		virtual public void Paint() {
			canvas.Apply ();
			for (int i = 0; i < subArtist.Length; i++)
				subArtist [i].Paint ();
			
		}			

	}
}