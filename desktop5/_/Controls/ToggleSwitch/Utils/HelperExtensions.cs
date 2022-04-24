﻿using System;

namespace desktop.Controls.ToggleSwitch.Utils
{
	public static class HelperExtensions
	{
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			return val.CompareTo(min) < 0 ? min : (val.CompareTo(max) > 0 ? max : val);
		}
	}
}