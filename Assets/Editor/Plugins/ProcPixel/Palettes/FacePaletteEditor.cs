using UnityEngine;
using System.Collections;
using UnityEditor;


namespace ProcPixel.Palettes {

	[CustomEditor(typeof(FacePalette), false)]
	public class FacePaletteEditor : Editor {

		public override void OnInspectorGUI ()
		{
			
			EditorGUI.indentLevel += 1;
			ProcPixel.Fundamentals.EnumEditorHelper<FaceColors>.UnpackProperty(serializedObject.FindProperty("colors"));

			EditorGUI.indentLevel -= 1;
		}
	}
}