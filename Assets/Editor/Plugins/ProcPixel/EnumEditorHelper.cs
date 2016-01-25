using UnityEngine;
using UnityEditor;

namespace ProcPixel.Fundamentals {

	public static class EnumEditorHelper<T> where T: System.IConvertible
	{

		public static void UnpackProperty(SerializedProperty prop)
		{
			
			int size = EnumeratedPalette<T>.EnumLength;
			for (int index = 0; index < size; index++) {
				EditorGUILayout.PropertyField (prop.GetArrayElementAtIndex (index), new GUIContent(EnumeratedPalette<T>.FieldLabel (index)));
			}
		}
	}
}
