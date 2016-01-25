using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {

	public delegate void NewCanvas(Sprite sprite);

	public class PaintCanvas : MonoBehaviour {

		public event NewCanvas OnNewCanvas;

		[SerializeField, HideInInspector]
		Sprite _current;

		public Sprite currentSprite {
			get {
				return _current;
			}
		}

		public string spriteName {
			get {
				if (_current == null)
					return "Generated Sprite";
				return _current.name;
			}

			set {
				_current.name = value;
			}
		}

		[SerializeField, HideInInspector]
		Texture2D _image;

		[SerializeField, HideInInspector]
		int width = 32;

		[SerializeField, HideInInspector]
		int height = 32;

		void Reset() {
			CreateNewCanvas ();
			if (GetComponent<UnityEngine.UI.Image> () != null)
				SetupConnector ();
		}

		void SetupConnector() {
			var connector = GetComponent<CanvasToImage> ();
			if (connector)
				connector.HookUp (this);
			else
				gameObject.AddComponent<CanvasToImage> ();
		}

		public void CreateNewCanvas() {
			_image = new Texture2D (width, height);
			var name = spriteName;;
			var rect = new Rect (Vector2.zero, new Vector2 (width, height));
			_current = Sprite.Create (_image, rect, Vector2.one * 0.5f);
			spriteName = name;

			if (OnNewCanvas != null)
				OnNewCanvas (_current);
		}

		public void Draw(int x, int y, Color32 color) {
			_image.SetPixel (x, y, color);
		}

	}
}