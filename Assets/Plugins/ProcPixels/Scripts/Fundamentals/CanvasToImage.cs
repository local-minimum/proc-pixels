using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ProcPixel.Fundamentals {

	[ExecuteInEditMode, DisallowMultipleComponent]
	public class CanvasToImage : MonoBehaviour {

		Image _image;
		PaintCanvas _canvas;

		void Awake() {
			if (!_canvas)
				Reset ();
		}

		void Reset() {
			_image = GetComponent<Image> ();
			HookUp (GetComponent<PaintCanvas> ());
		}

		public void HookUp(PaintCanvas canvas) {
			if (_canvas)
				_canvas.OnNewCanvas -= HandleNewCanvas;				
			_canvas = canvas;
			if (_canvas) {
				_image.sprite = _canvas.currentSprite;
				_canvas.OnNewCanvas += HandleNewCanvas;
			}
		}

		void HandleNewCanvas(Sprite sprite) {
			_image.sprite = sprite;
		}
	}
}