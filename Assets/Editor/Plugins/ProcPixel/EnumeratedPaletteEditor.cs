using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProcPixel.Fundamentals {

	[CustomEditor(typeof(EnumeratedPalette<>), true)]
	public class EnumeratedPaletteEditor<T> : Editor where T: System.IConvertible
	{
		public override void OnInspectorGUI ()
		{
			
			int size = EnumeratedPalette<T>.EnumLength;
			for (int index = 0; index < size; index++) {
				EditorGUILayout.PropertyField (serializedObject.FindProperty ("colors").GetArrayElementAtIndex (index), new GUIContent(EnumeratedPalette<T>.FieldLabel (index)));
			}
		}
	}
}
