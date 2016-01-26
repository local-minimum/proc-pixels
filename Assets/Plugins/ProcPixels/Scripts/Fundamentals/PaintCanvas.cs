using UnityEngine;
using System.Collections;
using System.IO;

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
			_image = new Texture2D (width, height, TextureFormat.RGBA32, false);
			_image.filterMode = FilterMode.Point;
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

		public void Apply() {
			_image.Apply ();
		}

		public void Clear() {
			Color32 col = new Color32 (0,0,0,0);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					_image.SetPixel (x, y, col);
				}
			}
			_image.Apply ();

		}

		public void Save() {
			string filename = spriteName;
			if (filename == "")
				filename = "proc_gen_demo";
			
			File.WriteAllBytes (filename + ".png", _image.EncodeToPNG ());
		}

	}
}