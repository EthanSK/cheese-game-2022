using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class TransformExtensions
    {
        public static Rect CalculateBoundingBox(this IEnumerable<Transform> points, Vector2 indivPosOffset = default(Vector2), Vector2 overallSizeOffset = default(Vector2))
        {
            Vector2 minPos = new Vector2();
            Vector2 maxPos = new Vector2();
            bool isFirstIteration = true;
            foreach (Transform point in points)
            {
                Vector2 pointPosition = (Vector2)point.position + indivPosOffset;
                if (isFirstIteration)
                {
                    minPos = pointPosition;
                    maxPos = pointPosition;
                    isFirstIteration = false;
                }
                else
                {
                    minPos = Vector2.Min(minPos, pointPosition);
                    maxPos = Vector2.Max(maxPos, pointPosition);
                }

            }
            return new Rect(minPos - overallSizeOffset / 2, overallSizeOffset + maxPos - minPos);
        }

        public static void DestroyChildren(this Transform parent)
        {
            int childCount = parent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var child = parent.transform.GetChild(i).gameObject;
                GameObject.Destroy(child.gameObject);
            }
        }

        public static bool IsFlippedHoriz(this Transform transform)
        {
            return transform.lossyScale.x < 0f;
        }

        public static void SetIsFlippedHoriz(this Transform transform, bool isFlipped)
        {
            Vector3 scale = transform.localScale;
            if (!isFlipped) scale.x = Mathf.Abs(scale.x);
            else scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        public static void SetLocalRotation(this Transform transform, float angle)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
}