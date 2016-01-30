using UnityEngine;
using System.Collections;

namespace ProcPixel.Artists.Face {
	
	public class StyleSelector : AbstractFace{

		[SerializeField, Range(0, 1)]
		float probabilityOfNothing = 0.3f;

		protected override void Fill ()
		{
			
		}

		protected override void SetColor ()
		{
			
		}

		public override void Paint ()
		{
			if (Random.value > probabilityOfNothing) {

			}
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			polygon = parentPolygon;

			if (Random.value > probabilityOfNothing) {

			}
		}

		protected virtual void RandomSelectSubArtis() {
			subArtist [Random.Range (0, subArtist.Length)].Paint (polygon);
		}

		protected override void SetPolygon ()
		{
			
		}

		protected override void SetNewValues ()
		{
			
		}

		protected override void PostProcessing ()
		{
			
		}

	}
}