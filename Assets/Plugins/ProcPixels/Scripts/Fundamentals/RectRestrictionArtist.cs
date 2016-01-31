using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals {
	public class RectRestrictionArtist : Artist {

		[SerializeField, Range(0, 10)]
		float aspectRatioMax = 1.2f;

		[SerializeField, Range(0, 10)]
		float aspectRatioMin = 0.8f;

		[SerializeField, Range(0, 1)]
		float paddingLeft;

		[SerializeField, Range(0, 1)]
		float paddingRight;

		[SerializeField, Range(0, 1)]
		float paddingTop;

		[SerializeField, Range(0, 1)]
		float paddingBottom;

		public void SetRect() {
			var rect = canvas.currentSprite.rect;
			float x0 = rect.xMin + paddingLeft * rect.width;
			float x1 = rect.xMax - paddingRight * rect.width;
			float y0 = rect.yMin + paddingBottom * rect.height;
			float y1 = rect.yMax - paddingTop * rect.height;

			float width = x1 - x0;
			float height = y1 - y0;
			float aspect = width / height;

			if (aspect < aspectRatioMin) {
				//Recalculate decrease hight
				var newHeight = width / aspectRatioMin;
				y0 += (height - newHeight) / 2f;
				y1 -= (height - newHeight) / 2f;
			} else if (aspect > aspectRatioMax) {
				//Recalculate decrease width
				var newWidth = height * aspectRatioMax;
				x0 += (width - newWidth) / 2f;
				x1 -= (width - newWidth) / 2f;
			}

			_rect = new Rect (x0, y0, x1 - x0, y1 - y0);

		}

		protected override void _Paint ()
		{
			SetRect ();
			polygon = new Vector2[4] {
				new Vector2 (_rect.xMin, _rect.yMin),
				new Vector2 (_rect.xMin, _rect.yMax),
				new Vector2 (_rect.xMax, _rect.yMax),
				new Vector2 (_rect.xMax, _rect.yMin)
			};
		}
	}
}