using OneManEscapePlan.Common.Scripts.DataStructures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Common.Scripts.DataStructures {
	public interface IReadOnlyAngleRange {
		Angle Min { get; }
		Angle Mid { get; }
		Angle Max { get; }
		float Size { get; }

		Angle Clamp(Angle value);
		bool Contains(Angle value);
		Angle GetRandom();
		Angle Lerp(float t);
	}

	/// <summary>
	/// Immutable struct representing a contiguous range of angles (i.e. an arc).
	/// </summary>
	/// <remarks>
	/// We can't use a FloatRange to represent angles, because it won't account for wrapping from 360°
	/// to 0°. For example, we could define a float range from 350 to 370 (-10° to 10°) but if we tried
	/// to clamp 11, it would give us 350 (-10°) because 11 is closer to 350 than to 370. In contrast,
	/// AngleRangeValue correctly accounts for wrapping from 360° to 0°.
	/// </remarks>
	[System.Serializable]
	public struct AngleRangeValue : IReadOnlyAngleRange {
		#region FIELDS
		[SerializeField] private Angle start;
		[SerializeField] private float size;

		/// <summary>
		/// Create a new AngleRangeValue
		/// </summary>
		/// <param name="start">Starting angle</param>
		/// <param name="size">Size of the range in degrees; must be positive</param>
		/// <exception cref="System.ArgumentException"></exception>
		/// <remarks>
		/// <c>size</c> is a float, rather than an Angle, to avoid confusion.
		/// </remarks>
		public AngleRangeValue(Angle start, float size) {
			if (size < 0) throw new System.ArgumentException($"Size cannot be negative (given {size})");
			if (size >= 360) {
				this.start = new Angle(0);
				this.size = 360;
			} else {
				this.start = start;
				this.size = size;
			}
		}

		public Angle Min => start;

		public Angle Mid => start + new Angle(size / 2);

		public Angle Max => start + new Angle(size);

		public float Size => size;

		public Angle Clamp(Angle value) {
			if (Size >= 360) return value;
			float min = Min.Degrees360;
			float mid = min + Size / 2;
			float v = value.Degrees360;
			float deltaMid = Mathf.DeltaAngle(v, mid);
			//Debug.Log($"Min: {min}, Mid: {mid}, v: {v}, deltaMid: {deltaMid}, size/2: {Size / 2}");
			if (Mathf.Abs(deltaMid) <= Size / 2) {
				return value;
			}
			if (deltaMid < 0) return Max;
			return Min;
		}

		public bool Contains(Angle value) {
			return Clamp(value) == value;
		}

		public Angle GetRandom() {
			return Lerp(UnityEngine.Random.value);
		}

		public Angle Lerp(float t) {
			return start + new Angle(size * t);
		}

		public override string ToString() {
			return $"({Min}-{Max})";
		}
		#endregion
	}

	/// <summary>
	/// Class representing a contiguous range of angles (i.e. an arc).
	/// </summary>
	/// <remarks>
	/// A mutable, reference-type wrapper for AngleRangeValue.
	/// </remarks>
	public class AngleRange : IReadOnlyAngleRange {
		[SerializeField] private AngleRangeValue value;

		/// <summary>
		/// Create a new AngleRange.
		/// </summary>
		/// <param name="start">Starting angle</param>
		/// <param name="size">Size of the range in degrees; must be positive</param>
		/// <exception cref="System.ArgumentException"></exception>
		public AngleRange(Angle start, float size) {
			SetRange(start, size);
		}

		public Angle Min => value.Min;

		public Angle Mid => value.Mid;

		public Angle Max => value.Max;

		public float Size => value.Size;

		public AngleRangeValue Value { get => value; set => this.value = value; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start">Starting angle</param>
		/// <param name="size">Size of the range in degrees; must be positive</param>
		public void SetRange(Angle start, float size) {
			value = new AngleRangeValue(start, size);
		}

		public Angle Clamp(Angle value) {
			return this.value.Clamp(value);
		}

		public bool Contains(Angle value) {
			return this.value.Contains(value);
		}

		public Angle GetRandom() {
			return value.GetRandom();
		}

		public Angle Lerp(float t) {
			return value.Lerp(t);
		}
	}
}