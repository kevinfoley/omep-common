/// ©2022 Kevin Foley. 
/// See accompanying license file.

using OneManEscapePlan.Common.Scripts.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace OneManEscapePlan.Common.Scripts.Editor {

	[CustomPropertyDrawer(typeof(AngleRange))]
	public class AngleRangePropertyDrawer : PropertyDrawer {

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (property.isExpanded) return base.GetPropertyHeight(property, label) * 2;
			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			label = EditorGUI.BeginProperty(position, label, property);
			Color defaultBackgroundColor = GUI.backgroundColor;

			var valueProperty = property.FindPropertyRelative("value");
			var minProperty = valueProperty.FindPropertyRelative("start").FindPropertyRelative("rawDegrees");
			var sizeProperty = valueProperty.FindPropertyRelative("size");

			if (sizeProperty.floatValue < 0) GUI.backgroundColor = Color.red;

			float defaultLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 42;

			Rect rect = position;
			if (property.isExpanded) rect.height /= 2f;

			var min = new Angle(minProperty.floatValue);
			var max = new Angle(minProperty.floatValue + sizeProperty.floatValue);
			label.text += $":  {min.ToString180()} - {max.ToString180()}";
			property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
			if (property.isExpanded) {
				int defaultIndentLevel = EditorGUI.indentLevel;
				int indent = (defaultIndentLevel + 1) * 10;
				EditorGUI.indentLevel = 0;

				rect.y += rect.height;
				rect.width = (rect.width - indent - 5) / 3f;
				rect.x += indent;

				minProperty.floatValue = EditorGUI.FloatField(rect, "Min", minProperty.floatValue);
				rect.x += rect.width + 5;
				float maxF = EditorGUI.FloatField(rect, "Max", minProperty.floatValue + sizeProperty.floatValue);
				sizeProperty.floatValue = maxF - minProperty.floatValue;
				rect.x += rect.width + 5;
				sizeProperty.floatValue = EditorGUI.FloatField(rect, "Size", sizeProperty.floatValue);

				EditorGUI.indentLevel = defaultIndentLevel;
			}

			GUI.backgroundColor = defaultBackgroundColor;
			EditorGUIUtility.labelWidth = defaultLabelWidth;

			EditorGUI.EndProperty();
		}
	}
}
