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

	[CustomPropertyDrawer(typeof(TimeSpanF))]
	public class TimeSpanFPropertyDrawer : PropertyDrawer {

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (property.isExpanded) return base.GetPropertyHeight(property, label) * 2;
			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			label = EditorGUI.BeginProperty(position, label, property);

			var secondsProperty = property.FindPropertyRelative("seconds");
			float totalSeconds = secondsProperty.floatValue;

			float defaultLabelWidth = EditorGUIUtility.labelWidth;

			Rect rect = position;
			if (property.isExpanded) rect.height /= 2f;

			TimeSpanF timeSpan = TimeSpanF.FromSeconds(totalSeconds);
			label.text += ":  " + timeSpan.ToString();

			property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
			if (property.isExpanded) {
				EditorGUIUtility.labelWidth = 45;
				rect.y += rect.height;
				rect.width = (rect.width - 18) / 3f - 8;
				rect.x += 10;

				int hours = EditorGUI.IntField(rect, "Hours", timeSpan.Hours);

				EditorGUIUtility.labelWidth = 50;
				rect.x += rect.width + 4;
				rect.width += 4;
				int minutes = EditorGUI.IntField(rect, "Minutes", timeSpan.Minutes);

				EditorGUIUtility.labelWidth = 55;
				rect.x += rect.width + 4;
				rect.width += 16;
				float seconds = EditorGUI.FloatField(rect, "Seconds", timeSpan.Seconds);

				TimeSpanF newTimeSpan = new TimeSpanF(hours, minutes, seconds);
				if (newTimeSpan != timeSpan) {
					secondsProperty.floatValue = newTimeSpan.TotalSeconds;
				}
			}

			EditorGUIUtility.labelWidth = defaultLabelWidth;

			EditorGUI.EndProperty();
		}
	}
}
