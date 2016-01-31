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
			EditorGUILayout.IntSlider (serializedObject.FindProperty ("_width"), 1, 256);
			EditorGUILayout.IntSlider (serializedObject.FindProperty ("_height"), 1, 256);

			if (EditorGUI.EndChangeCheck()) {				
				serializedObject.ApplyModifiedPropertiesWithoutUndo ();
				canvas.CreateNewCanvas ();
			}

			if (GUILayout.Button ("New (doesn't overwrite previous if use elsewhere)"))
				canvas.CreateNewCanvas ();
			
			if (GUILayout.Button ("Clear/Overwrite current as blank"))
				canvas.Clear ();

			if (GUILayout.Button ("Save as PNG"))
				canvas.Save ();
		}
	}
}