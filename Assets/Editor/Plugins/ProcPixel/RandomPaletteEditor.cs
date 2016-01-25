using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProcPixel.Fundamentals {
	
	[CustomEditor(typeof(RandomColorPalette), true)]
	public class RandomPaletteEditor : Editor {

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.HelpBox ("Size is the pretended size, useful if palette is part of a meta palette, to adjust the probability of random colors.", MessageType.Info);
			EditorGUILayout.IntSlider (serializedObject.FindProperty ("_size"), 1, 100);
		}
	}
}
