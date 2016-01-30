using UnityEngine;
using System.Collections.Generic;
using ProcPixel.Palettes;
using ProcPixel.Utils;
using ProcPixel.Fundamentals;

namespace ProcPixel.Artists.Face {
	
	enum HairWaveDirection {Left, Right, Random};

	public class NaiveHairTopWave : AbstractFace {

		Vector2[] parentHead;

		[SerializeField, Range(0.05f, 1)]
		float baseWidth = 0.05f;

		[SerializeField, Range(0, 0.5f)]
		float baseDepression = 0;

		[SerializeField, Range(0.1f, 2f)]
		float hairHight = 0.4f;

		[SerializeField, Range(-0.2f, 0.2f)]
		float lateralOffset = 0;

		[SerializeField, Range(0f, 0.25f)]
		float curliness = 0.05f;

		[SerializeField]
		HairWaveDirection direction = HairWaveDirection.Left;
			
		protected override void SetNewValues ()
		{
			baseWidth = Random.Range (0.05f, 1);
			baseDepression = Random.Range (0, 0.5f);
			hairHight = Random.Range (0.1f, 2f);
			lateralOffset = Random.Range (-0.2f, 0.2f);
			curliness = Random.Range (0f, 0.25f);

			var enumValues = (HairWaveDirection[]) System.Enum.GetValues (typeof(HairWaveDirection));
			direction = enumValues[Random.Range(0, enumValues.Length)];

		}

		protected override void SetPolygon ()
		{
			if (parentHead == null)
				throw new System.ArgumentNullException ("Whoever heard of hair without its head!");


			float yScale = ((parentHead[3].y - parentHead[2].y) + (parentHead[4].y - parentHead[5].y)) * 0.5f;
			float xScale = (parentHead [5].x - parentHead [2].x);

			Vector2 center = (parentHead [3] + parentHead [4]) * 0.5f + Vector2.down * yScale * baseDepression + xScale * lateralOffset * Vector2.right;

			float leftX = center.x - xScale * 0.5f * baseWidth;
			float rightX = center.x + xScale * 0.5f * baseWidth;

			var polygon = new List<Vector2> ();

			if (leftX > parentHead [3].x) {
				polygon.Add(Vector2.Lerp(parentHead[3] + Vector2.down * yScale * baseDepression, center, baseWidth /2f));
			} else {
				polygon.Add(Vector2.Lerp(parentHead[3], parentHead[2], baseWidth) + Vector2.down * yScale * baseDepression);
				polygon.Add (parentHead [3] + Vector2.down * yScale * baseDepression);
			}

			if (rightX < parentHead [4].x) {
				polygon.Add(Vector2.Lerp(center, parentHead[4] + Vector2.down * yScale * baseDepression, baseWidth /2f));
			} else {
				polygon.Add (parentHead [4] + Vector2.down * yScale * baseDepression);
				polygon.Add(Vector2.Lerp(parentHead[4] , parentHead[5], baseWidth) + Vector2.down * yScale * baseDepression);

			}

			this.polygon = polygon.ToArray ();

			if (noise > 0)
				ApplyNoise ();
		}

		protected override void Fill ()
		{
			var line = LineMath.VectorLineToPixels (canvasWidth, canvasHeight, AdjancanyCondition.Line, polygon);	
			int growthIterations = Mathf.RoundToInt (hairHight * canvasHeight);
			float rotationAcceleration = 0;
			if (direction == HairWaveDirection.Left)
				rotationAcceleration = 1;
			else if (direction == HairWaveDirection.Right)
				rotationAcceleration = -1;
					
			for (int i = 0; i < line.Length; i++) {
				//Build hair
				if (direction == HairWaveDirection.Random)
					rotationAcceleration = Random.value < 0.5f ? 1f : -1f;
				
				var hair = LineMath.GrowSnake (
					           canvasWidth, canvasHeight, line [i], Vector2.up * 0.2f,
					           rotationAcceleration * curliness,
					           growthIterations);

				var shade = ProcPixel.Fundamentals.Color.RandomShade;
				for (int j = 0; j < hair.Length; j++) {
					Draw (hair [j], shade);
				}
			}
		}

		protected override void PostProcessing ()
		{
			
		}

		protected override void SetColor ()
		{
			color = _palette [FaceColors.Hair];
		}

		public override void Paint (Vector2[] parentPolygon)
		{
			parentHead = parentPolygon;
			base.Paint ();
		}
	}
}