/// ©2021 Kevin Foley. 
/// See accompanying license file.

using System;
using UnityEditor;
using UnityEngine;

namespace OneManEscapePlan.Common.Scripts.Editor {
	/// <summary>
	/// Simpler wrapper for EditorGUILayout.BeginHorizontal() / EditorGUILayout.EndHorizontal().
	/// Requires allocating an object and thus generates a tiny amount of extra memory garbage,
	/// but it's Editor-only so that's not a big deal.
	/// </summary>
	/// <example>
	/// <code>
	///	using (var layout = new HorizontalGUILayout("box")) {
	///		useDelay = EditorGUILayout.Toggle("Use delay", useDelay);
	///		if (useDelay) delay = EditorGUILayout.FloatField(delay);
	///	}
	/// </code>
	/// </example>
	public class HorizontalGUILayout : IDisposable {
		public HorizontalGUILayout() {
			EditorGUILayout.BeginHorizontal();
		}

		public HorizontalGUILayout(params GUILayoutOption[] options) {
			EditorGUILayout.BeginHorizontal(options);
		}

		public HorizontalGUILayout(GUIStyle style, params GUILayoutOption[] options) {
			EditorGUILayout.BeginHorizontal(style, options);
		}

		public void Dispose() {
			EditorGUILayout.EndHorizontal();
		}
	}

	/// <summary>
	/// Simpler wrapper for EditorGUILayout.BeginVertical() / EditorGUILayout.EndVertical().
	/// Requires allocating an object and thus generates a tiny amount of extra memory garbage,
	/// but it's Editor-only so that's not a big deal.
	/// </summary>
	/// <example>
	/// <code>
	///	using (var layout = new VerticalGUILayout("box)) {
	///		name = EditorGUILayout.TextField("Name", name);
	///		hp = EditorGUILayout.IntSlider("HP", hp, 0, 100);
	///	}
	/// </code>
	/// </example>
	public class VerticalGUILayout : IDisposable {
		public VerticalGUILayout() {
			EditorGUILayout.BeginVertical();
		}

		public VerticalGUILayout(params GUILayoutOption[] options) {
			EditorGUILayout.BeginVertical(options);
		}

		public VerticalGUILayout(GUIStyle style, params GUILayoutOption[] options) {
			EditorGUILayout.BeginVertical(style, options);
		}

		public void Dispose() {
			EditorGUILayout.EndVertical();
		}
	}
}
