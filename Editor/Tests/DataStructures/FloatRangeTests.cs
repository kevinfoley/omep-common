/// ©2021 Kevin Foley. 
/// See accompanying license file.

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OneManEscapePlan.Common.Scripts.DataStructures;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
	public class FloatRangeTests {
		[Test]
		public void Clamp() {
			Assert.IsTrue(new FloatRange(5, 10).Clamp(0) == 5);
			Assert.IsTrue(new FloatRange(5, 10).Clamp(15) == 10);
			Assert.IsTrue(new FloatRange(5, 10).Clamp(8) == 8);
			Assert.IsTrue(new FloatRange(-10, 10).Clamp(-20) == -10);
			Assert.IsTrue(new FloatRange(-10, 10).Clamp(8) == 8);
			Assert.IsTrue(new FloatRange(-10, 10).Clamp(18) == 10);
		}

		[Test]
		public void Normalize() {
			FloatRange range = new FloatRange(0, 20);

			Assert.IsTrue(range.Normalize(-5, true) == 0);
			Assert.IsTrue(range.Normalize(-5, false) == -.25f);
			Assert.IsTrue(range.Normalize(5, true) == .25f);
			Assert.IsTrue(range.Normalize(5, false) == .25f);
			Assert.IsTrue(range.Normalize(10, true) == .5f);
			Assert.IsTrue(range.Normalize(10, false) == .5f);
			Assert.IsTrue(range.Normalize(20, true) == 1);
			Assert.IsTrue(range.Normalize(20, false) == 1);
			Assert.IsTrue(range.Normalize(25, true) == 1);
			Assert.IsTrue(range.Normalize(25, false) == 1.25);

			range = new FloatRange(-20, 5);

			Assert.IsTrue(range.Normalize(0, true) == .8f);
			Assert.IsTrue(range.Normalize(0, false) == .8f);
			Assert.IsTrue(range.Normalize(-20, true) == 0f);
			Assert.IsTrue(range.Normalize(-20, false) == 0f);
			Assert.IsTrue(range.Normalize(-25, true) == 0f);
			Assert.IsTrue(range.Normalize(-25, false) == -.2f);
			Assert.IsTrue(range.Normalize(5, true) == 1f);
			Assert.IsTrue(range.Normalize(5, false) == 1f);
			Assert.IsTrue(range.Normalize(10, true) == 1);
			Assert.IsTrue(range.Normalize(10, false) == 1.2f);
		}

		[Test]
		public void Contains() {
			FloatRange range = new FloatRange(-15, 20);

			Assert.IsTrue(range.Contains(-15));
			Assert.IsTrue(range.Contains(-10));
			Assert.IsTrue(range.Contains(0));
			Assert.IsTrue(range.Contains(10));
			Assert.IsTrue(range.Contains(20));

			Assert.IsFalse(range.Contains(-20));
			Assert.IsFalse(range.Contains(25));
		}

		[Test]
		public void Lerp() {
			FloatRange range = new FloatRange(-20, 20);

			Assert.IsTrue(range.Lerp(0) == -20);
			Assert.IsTrue(range.Lerp(.25f) == -10);
			Assert.IsTrue(range.Lerp(.5f) == 0);
			Assert.IsTrue(range.Lerp(.75f) == 10);
			Assert.IsTrue(range.Lerp(1f) == 20);
		}

		[Test]
		public void Operators() {
			Assert.IsTrue(new FloatRange(22, 33).Equals(new FloatRange(22, 33)));
			Assert.IsFalse(new FloatRange(22, 33).Equals(new FloatRange(-22, 33)));
		}
	}
}
