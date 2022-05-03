﻿/// ©2022 Kevin Foley. 
/// See accompanying license file.

using OneManEscapePlan.Common.Scripts.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace OneManEscapePlan.Common.Scripts.Editor {

	[CustomPropertyDrawer(typeof(Mass))]
	public class MassPropertyDrawer : PropertyDrawer {

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (property.isExpanded) return base.GetPropertyHeight(property, label) * 2;
			return base.GetPropertyHeight(property, label);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			label = EditorGUI.BeginProperty(position, label, property);

			var kgProperty = property.FindPropertyRelative("kilograms");
			float kg = kgProperty.floatValue;

			float defaultLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 20;

			Rect rect = position;
			if (property.isExpanded) rect.height /= 2f;

			Mass mass = Mass.FromKilograms(kg);
			label.text += ":  " + mass.ToString("F2");

			property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
			if (property.isExpanded) {
				int defaultIndentLevel = EditorGUI.indentLevel;
				int indent = (defaultIndentLevel + 1) * 10;
				EditorGUI.indentLevel = 0;

				rect.y += rect.height;
				rect.width = (rect.width - indent - 8) / 3f - 5;
				rect.x += indent;

				kg = EditorGUI.FloatField(rect, "kg", kg);
				if (kg != mass.Kilograms) {
					kgProperty.floatValue = kg;
				}

				rect.x += rect.width + 4;
				float lbs = EditorGUI.FloatField(rect, "lbs", mass.Pounds);
				if (lbs != mass.Pounds) {
					Mass newMass = Mass.FromPounds(lbs);
					kgProperty.floatValue = newMass.Kilograms;
				}

				EditorGUIUtility.labelWidth += 20;
				rect.x += rect.width + 4;
				rect.width += 14;
				float tons = EditorGUI.FloatField(rect, "us ton", mass.ShortTons);
				if (tons != mass.ShortTons) {
					Mass newMass = Mass.FromShortTons(tons);
					kgProperty.floatValue = newMass.Kilograms;
				}

				EditorGUI.indentLevel = defaultIndentLevel;
			}

			EditorGUIUtility.labelWidth = defaultLabelWidth;

			EditorGUI.EndProperty();
		}
	}
}
