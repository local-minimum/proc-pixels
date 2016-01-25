using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProcPixel.Fundamentals {
	
	[CustomEditor(typeof(RandomColorPalette), true)]
	public class RandomPaletteEditor : Editor {

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.HelpBox ("Pretends to be a one length palette that will return any color", MessageType.Info);
		}
	}
}
