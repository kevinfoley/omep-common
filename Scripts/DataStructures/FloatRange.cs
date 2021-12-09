/// ©2021 Kevin Foley. 
/// See accompanying license file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OneManEscapePlan.Common.Scripts.DataStructures {

	public interface IReadOnlyFloatRange {
		float Min { get; }
		float Mid { get; }
		float Max { get; }

		float Clamp(float value);
		bool Contains(float value);
		float GetRandom();
		float Lerp(float t);
		float Normalize(float value, bool clamp = false);
	}

	[System.Serializable]
	public class FloatRange : IReadOnlyFloatRange {
		[SerializeField] protected float min = 0;
		[SerializeField] protected float max = 1;

		public FloatRange(float min, float max) {
			this.min = min;
			this.max = max;
		}

		public float Min {
			get => min;
		}

		/// <summary>
		/// Midpoint between Min and Max
		/// </summary>
		public float Mid {
			get => (min + max) / 2f;
		}

		public float Max { 
			get => max;
		}

		public void SetRange(float min, float max) {
			if (max < min) throw new ArgumentException("Max cannot be less than min (provided values: " + min + ", " + max + ")");
			this.min = min;
			this.max = max;
		}

		public float Clamp(float value) {
			return Mathf.Clamp(value, min, max);
		}

		public float GetRandom() {
			return UnityEngine.Random.Range(min, max);
		}

		public float Lerp(float t) {
			return Mathf.Lerp(min, max, t);
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

		public override bool Equals(object obj) {
			return obj is FloatRange range &&
				   min == range.min &&
				   max == range.max;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public FloatRange Clone() {
			return new FloatRange(min, max);
		}

		public override string ToString() {
			return "(" + min + " - " + max + ")";
		}
		
		public string ToString(string format) {
			return "(" + min.ToString(format) + " - " + max.ToString(format) + ")";
		}
	}
}
