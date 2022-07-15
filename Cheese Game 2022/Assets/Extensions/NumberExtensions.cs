using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ETGgames.Extensions
{
    public static class FloatExtensions
    {
        public static Vector3 ToVector3(this float value)
        {
            return new Vector3(value, value, value);
        }
        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }

        public static int MathematicalModulus(this int a, int b)
        {
            return ((a % b) + b) % b;
        }

    }
}
