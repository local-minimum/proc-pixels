using UnityEngine;
using UnityEditor;

namespace ProcPixel.Fundamentals
{

	[CustomEditor(typeof(MetaPalette), true)]
	public class MetaPaletteEditor : Editor {
		public override void OnInspectorGUI ()
		{
			var prop = serializedObject.FindProperty ("palettes");
			EditorGUILayout.PropertyField (prop);
			EditorGUI.indentLevel += 1;
			if (prop.isExpanded) {
				prop.arraySize = EditorGUILayout.IntField ("Size", prop.arraySize);
				for (int i = 0, l = prop.arraySize; i < l; i++)
					EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex (i));	
			}
			serializedObject.ApplyModifiedProperties ();
			EditorGUI.indentLevel -= 1;
		}
	}
}