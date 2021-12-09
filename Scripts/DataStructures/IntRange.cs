﻿/// ©2021 Kevin Foley. 
/// See accompanying license file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OneManEscapePlan.Common.Scripts.DataStructures {
	public interface IReadOnlyIntRange {
		int Min { get; }
		int Mid { get; }
		int Max { get; }

		int Clamp(int value);
		bool Contains(float value);
		bool Contains(int value);
		int GetRandom(bool inclusive = true);
		float Lerp(float t);
		int LerpRounded(float t);
		float Normalize(float value, bool clamp = false);
	}

	[System.Serializable]
	public class IntRange : IReadOnlyIntRange {
		[SerializeField] protected int min = 0;
		[SerializeField] protected int max = 100;

		public IntRange(int min, int max) {
			this.min = min;
			this.max = max;
		}

		public int Min {
			get => min;
		}

		/// <summary>
		/// Midpoint between Min and Max
		/// </summary>
		public int Mid {
			get => (min + max) / 2;
		}

		public int Max { 
			get => max;
		}

		public void SetRange(int min, int max) {
			if (max < min) throw new ArgumentException("Max cannot be less than min (provided values: " + min + ", " + max + ")");
			this.min = min;
			this.max = max;
		}

		public int Clamp(int value) {
			return Mathf.Clamp(value, min, max);
		}

		/// <summary>
		/// Get a random value between <c>Min</c> and <c>Max</c>
		/// </summary>
		/// <param name="inclusive">Whether the <c>Max</c> value is inclusive (true) or exclusive (false)</param>
		/// <returns>An integer</returns>
		public int GetRandom(bool inclusive = true) {
			if (inclusive) return UnityEngine.Random.Range(min, max + 1);
			return UnityEngine.Random.Range(min, max);
		}

		public float Lerp(float t) {
			float value = Mathf.Lerp(min, max, t);
			return value;
		}

		public int LerpRounded(float t) {
			float value = Mathf.Lerp(min, max, t);
			return Mathf.RoundToInt(value);
		}

		/// <summary>
		/// Convert the given value to a normalized value relative to the range
		/// </summary>
		/// <param name="value"></param>
		/// <param name="clamp">If true, the normalized return value is clamped in range 0 - 1.
		/// If false, the return value may be less than 0 or greater than 1 if the input value is outside of the range.</param>
		/// <returns>A normalized value</returns>
		public float Normalize(float value, bool clamp = false) {
			float t = (value - min) / (max - min);
			if (clamp) t = Mathf.Clamp01(t);
			return t;
		}

		public bool Contains(float value) {
			return (value >= min && value <= max);
		}

		public bool Contains(int value) {
			return (value >= min && value <= max);
		}

		public override bool Equals(object obj) {
			return obj is IntRange range &&
				   min == range.min &&
				   max == range.max;
		}

		public IntRange Clone() {
			return new IntRange(min, max);
		}

		public override int GetHashCode() {
			var hashCode = -897720056;
			hashCode = hashCode * -1521134295 + min.GetHashCode();
			hashCode = hashCode * -1521134295 + max.GetHashCode();
			return hashCode;
		}
	}
}
