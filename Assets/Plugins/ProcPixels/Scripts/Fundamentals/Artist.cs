using UnityEngine;
using System.Collections;
using ProcPixel.Utils;

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

		public int canvasWidth {
			get {
				return canvas.width;
			}
		}

		public int canvasHeight {
			get {
				return canvas.height;
			}
		}

		protected Rect _rect;
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
			int pos = LineMath.PixelCoordinateToPosition (x, y, canvasWidth);
			if (pos < drawingLayer.Length)
				drawingLayer [pos] = shade;
			else {
				throw new System.IndexOutOfRangeException(
					string.Format("Position ({0}, {1}) that translates to index {2} is outside drawing layer (size {3}) of canvas (size {4} x {5})",
						x, y, pos, drawingLayer.Length, canvasWidth, canvasHeight));
			}
		}

		protected void Draw(int pos, ColorShade shade) {
			drawingLayer [pos] = shade;
		}

		protected virtual void HandleNewCanvas(Sprite sprite) {
			_rect = sprite.rect;
			drawingLayer = new ColorShade[canvasWidth * canvasHeight];

		}
			
		bool SetupCanvas() {
			if (canvas == null) {
				canvas = GetComponentInParent<PaintCanvas> ();
				return canvas != null;
			}
			return true;
		}

		public void Paint() {

			if (!SetupCanvas()) {
				throw new System.ArgumentException ("Canvas isn't correctly initialized");
			}

			_Paint ();

			for (int i = 0; i < subArtist.Length; i++)
				subArtist [i].Paint (polygon);
			
		}

		virtual protected void _Paint() {

		}

		public void ClearCanvas() {
			canvas.Clear ();
		}

		public void ApplyToCanvas() {

			if (!SetupCanvas()) {
				throw new System.ArgumentException ("Canvas isn't correctly initialized");
			}

			for (int i = 0; i < drawingLayer.Length; i++) {
				if (drawingLayer[i] != ColorShade.None) {					
					canvas.Draw(LineMath.PostionToPixelCoordinate(i, canvasWidth), _color[drawingLayer[i]]); 
				}
			}

			canvas.Apply ();

			for (int i = 0; i < subArtist.Length; i++) {
				subArtist [i].ApplyToCanvas ();
			}
		}

		virtual public void Paint(Vector2[] parentPolygon) {
			Paint ();
		}

		#if UNITY_EDITOR
		void OnDrawGizmosSelected() {
			if (polygon == null)
				return;

			float sphereSize = 0.3f;
			float size = 4f;
			for (int i = 0; i < polygon.Length; i++) {
				int j = (i + 1) % polygon.Length;
				Gizmos.DrawLine (transform.TransformPoint (polygon [i] * size ), transform.TransformPoint (polygon [j] * size));
				Gizmos.DrawSphere(transform.TransformPoint(polygon[i] * size), sphereSize * size);

			}
		}

		#endif
	}
}