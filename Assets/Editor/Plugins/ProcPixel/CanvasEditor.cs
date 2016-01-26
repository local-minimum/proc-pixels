using UnityEngine;
using UnityEditor;

namespace ProcPixel.Fundamentals {

	[CustomEditor(typeof(PaintCanvas), true)]
	public class CanvasEditor : Editor {
		public override void OnInspectorGUI ()
		{
			PaintCanvas canvas = target as PaintCanvas;
			base.OnInspectorGUI ();
			canvas.spriteName = EditorGUILayout.TextField ("Sprite Name", canvas.spriteName);

			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.IntSlider (serializedObject.FindProperty ("width"), 1, 256);
			EditorGUILayout.IntSlider (serializedObject.FindProperty ("height"), 1, 256);

			if (EditorGUI.EndChangeCheck()) {				
				serializedObject.ApplyModifiedPropertiesWithoutUndo ();
				canvas.CreateNewCanvas ();
			}

			if (GUILayout.Button ("Clear"))
				canvas.Clear ();

			if (GUILayout.Button ("Save as PNG"))
				canvas.Save ();
		}
	}
}