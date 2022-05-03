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

	[CustomPropertyDrawer(typeof(FloatRange))]
	public class FloatRangePropertyDrawer : PropertyDrawer {

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (property.isExpanded) return base.GetPropertyHeight(property, label) * 2;
			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			label = EditorGUI.BeginProperty(position, label, property);
			Color defaultBackgroundColor = GUI.backgroundColor;

			var minProperty = property.FindPropertyRelative("min");
			var maxProperty = property.FindPropertyRelative("max");

			if (maxProperty.floatValue < minProperty.floatValue) GUI.backgroundColor = Color.red;

			float defaultLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 50;

			Rect rect = position;
			if (property.isExpanded) rect.height /= 2f;

			label.text += ":  " + minProperty.floatValue.ToString("F1") + " - " + maxProperty.floatValue.ToString("F1");
			property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
			if (property.isExpanded) {
				int defaultIndentLevel = EditorGUI.indentLevel;
				int indent = (defaultIndentLevel + 1) * 10;
				EditorGUI.indentLevel = 0;

				rect.y += rect.height;
				rect.width = (rect.width - indent - 5) / 2f;
				rect.x += indent;

				minProperty.floatValue = EditorGUI.FloatField(rect, "Min", minProperty.floatValue);
				rect.x += rect.width + 5;
				maxProperty.floatValue = EditorGUI.FloatField(rect, "Max", maxProperty.floatValue);

				EditorGUI.indentLevel = defaultIndentLevel;
			}

			GUI.backgroundColor = defaultBackgroundColor;
			EditorGUIUtility.labelWidth = defaultLabelWidth;

			EditorGUI.EndProperty();
		}
	}
}
