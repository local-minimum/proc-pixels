using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProcPixel.Fundamentals
{
    [CustomPropertyDrawer(typeof(Color))]
    public class ColorPropDrawer : PropertyDrawer
    {
		float rowHeight = 16f;
		float rowSpacing = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
			label = EditorGUI.BeginProperty (position, label, property);
			float positionWidth = position.width;

			position.width = 20f;
			property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, GUIContent.none);
			position.x += position.width;
			position.width = positionWidth * 0.8f;
			EditorGUI.LabelField(position, label);
			position.width = 20f;
			position.y += rowHeight + rowSpacing;

			if (property.isExpanded) {
				Rect contentPosition = new Rect();
				contentPosition.x = position.x;
				contentPosition.height = rowHeight;
				contentPosition.y = position.y;
				contentPosition.width = positionWidth * .85f;
				EditorGUI.indentLevel += 1;

				EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("referenceColor"), new GUIContent("Color"));
				contentPosition.y += rowHeight + rowSpacing;
				EditorGUI.BeginChangeCheck ();
				var shade = EditorGUI.Slider(contentPosition, GUIContent.none, property.FindPropertyRelative ("shading").floatValue, 0f, 1f);
				if (EditorGUI.EndChangeCheck ()) {
					var refColor = property.FindPropertyRelative ("referenceColor").colorValue;
					property.FindPropertyRelative ("shading").floatValue = shade;
					property.FindPropertyRelative ("darkerColor").colorValue = Color.CalculateShade (refColor, shade, ColorShade.Darker);
					property.FindPropertyRelative ("lighterColor").colorValue = Color.CalculateShade (refColor, shade, ColorShade.Lighter);
				}
				contentPosition.y += rowHeight + rowSpacing;
				EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("lighterColor"), new GUIContent("Lighter"));
				contentPosition.y += rowHeight + rowSpacing;
				EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("darkerColor"), new GUIContent("Darker"));

			} else {
				Rect contentPosition = new Rect();
				contentPosition.x = position.x + position.width;
				contentPosition.height = rowHeight;
				contentPosition.y = position.y;
				contentPosition.width = positionWidth * 0.3f;
				EditorGUIUtility.labelWidth = 30f;
				EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("referenceColor"), GUIContent.none);
				contentPosition.x += contentPosition.width;
				contentPosition.width = positionWidth * 0.6f;
				EditorGUI.BeginChangeCheck ();
				var shade = EditorGUI.Slider(contentPosition, GUIContent.none, property.FindPropertyRelative ("shading").floatValue, 0f, 1f);
				if (EditorGUI.EndChangeCheck ()) {
					var refColor = property.FindPropertyRelative ("referenceColor").colorValue;
					property.FindPropertyRelative ("shading").floatValue = shade;
					property.FindPropertyRelative ("darkerColor").colorValue = Color.CalculateShade (refColor, shade, ColorShade.Darker);
					property.FindPropertyRelative ("lighterColor").colorValue = Color.CalculateShade (refColor, shade, ColorShade.Lighter);
				}
			}

			EditorGUI.EndProperty ();
        }
			

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight (property, label) + (property.isExpanded ? 4: 1.5f) * (rowHeight + rowSpacing);
		}

    }
}