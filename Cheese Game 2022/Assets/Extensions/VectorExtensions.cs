using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ETGgames.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector3 vector, float? x, float? y, float? z)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        public static Vector3 WithX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }
        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }
        public static Vector2 WithY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }

        //converts to vector 3 so it can add a z
        public static Vector3 WithZ(this Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static float DegreesToOtherPoint(this Vector2 fromPoint, Vector2 toPoint)
        {
            return RadiansToOtherPoint(fromPoint, toPoint) * Mathf.Rad2Deg;
        }
        public static float RadiansToOtherPoint(this Vector2 fromPoint, Vector2 toPoint)
        {
            return Mathf.Atan2(toPoint.y - fromPoint.y, toPoint.x - fromPoint.x);
        }

        public static Vector3 Abs(this Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }

        public static Vector2 DegreesToVector2(this float degrees)
        {
            return RadiansToVector2(degrees * Mathf.Deg2Rad);
        }

        public static Vector2 RadiansToVector2(this float radians)
        {
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)); //cos and sin flipped so 0 is right not up
        }

        public static Vector3 MultiplyComponentWise(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }


    }
}
