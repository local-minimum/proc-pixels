using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProcPixel.Palettes {

	[CustomEditor(typeof(FaceMetaPalette), false)]
	public class FaceMetaPaletteEditor : Editor {

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.LabelField ("Palettes");
			EditorGUI.indentLevel += 1;
			ProcPixel.Fundamentals.EnumEditorHelper<FaceColors>.UnpackProperty(serializedObject.FindProperty("palettes"));
			EditorGUI.indentLevel -= 1;

			EditorGUILayout.Space ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Colors");
			if (GUILayout.Button ("New"))
				(target as FaceMetaPalette).SetRandomColorsFromPalettes ();
			EditorGUILayout.EndHorizontal ();

			EditorGUI.indentLevel += 1;
				ProcPixel.Fundamentals.EnumEditorHelper<FaceColors>.UnpackProperty(serializedObject.FindProperty("colors"));
			EditorGUI.indentLevel -= 1;
			serializedObject.ApplyModifiedProperties ();
		}

	}
}