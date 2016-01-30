using UnityEngine;
using UnityEditor;

namespace ProcPixel.Artists.Face {

	[CustomEditor(typeof(StyleSelector), true)]
	public class StyleSelectorEditor : Editor {

		public override void OnInspectorGUI ()
		{

			var artists = serializedObject.FindProperty ("subArtist");
			EditorGUILayout.PropertyField (artists);
			if (artists.isExpanded) {
				EditorGUI.indentLevel += 1;
				artists.arraySize = EditorGUILayout.IntField ("Size", artists.arraySize);

				for (int i = 0; i < artists.arraySize; i++) {
					EditorGUILayout.PropertyField (artists.GetArrayElementAtIndex (i));
				}
				EditorGUI.indentLevel -= 1;
			}

			EditorGUILayout.Slider(serializedObject.FindProperty ("probabilityOfNothing"), 0, 1);

			serializedObject.ApplyModifiedProperties ();
		}
	}
}