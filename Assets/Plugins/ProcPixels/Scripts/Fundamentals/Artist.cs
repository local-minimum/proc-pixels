using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {
	public class Artist : MonoBehaviour {

		[SerializeField, HideInInspector]
		PaintCanvas _canvas;

		[SerializeField]
		protected Artist[] subArtist;

		[SerializeField, HideInInspector]
		protected ColorShade[] drawingLayer;

		protected Vector2[] polygon;

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

		protected void ClearDrawingLayer() {
			for (int i = 0; i < drawingLayer.Length; i++)
				drawingLayer [i] = ColorShade.None;
		}

		protected void Draw(int x, int y, ColorShade shade) {
			drawingLayer [y * _canvasWidth + x] = shade;
		}

		protected void Draw(int pos, ColorShade shade) {
			drawingLayer [pos] = shade;
		}

		void HandleNewCanvas(Sprite sprite) {
			_rect = sprite.rect;
			_canvasWidth = Mathf.RoundToInt(sprite.rect.width);
			_canvasHeight = Mathf.RoundToInt (sprite.rect.height);
			drawingLayer = new ColorShade[_canvasWidth * _canvasHeight];

		}
	
		virtual public void Paint() {
			
			for (int i = 0; i < drawingLayer.Length; i++) {
				if (drawingLayer[i] != ColorShade.None) {
					int x = i % _canvasWidth;
					canvas.Draw(x, (i - x)/_canvasWidth, _color[drawingLayer[i]]); 
				}
			}

			canvas.Apply ();

			for (int i = 0; i < subArtist.Length; i++)
				subArtist [i].Paint (polygon);
			
		}			

		virtual public void Paint(Vector2[] parentPolygon) {
			Paint ();
		}
	}
}