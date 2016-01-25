using UnityEngine;
using UnityEditor;

namespace ProcPixel.Fundamentals {

	[CustomEditor(typeof(Artist), true)]
	public class ArtistEditor : Editor {
		public override void OnInspectorGUI ()
		{
			Artist myTarget = target as Artist;

			base.OnInspectorGUI ();

			EditorGUI.BeginChangeCheck ();
			PaintCanvas canvas = (PaintCanvas) EditorGUILayout.ObjectField ("Canvas", myTarget.canvas, typeof(PaintCanvas), true);
			if (EditorGUI.EndChangeCheck ())
				myTarget.canvas = canvas;

			if (GUILayout.Button ("Paint"))
				myTarget.Paint ();
		}
	}
}