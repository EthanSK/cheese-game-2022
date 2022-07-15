using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class GameObjectExtensions
    {
        public static float CalculateMaxDistance(this IList<GameObject> objects)
        {
            float maxDistance = 0f;
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < objects.Count - i; j++) //we do -i so we don't combine the same thing again
                {
                    float distance = (objects[i].transform.position - objects[j].transform.position).sqrMagnitude;
                    if (distance > maxDistance) maxDistance = distance;
                }
            }
            return Mathf.Sqrt(maxDistance); //more efficient that using Vector.Distance() which sqrts every loop
        }


        public static bool IsInTopmostLoadedScene(this GameObject gameObject)
        {
            return UnityEngine.SceneManagement.SceneManager.GetSceneAt(UnityEngine.SceneManagement.SceneManager.sceneCount - 1) == gameObject.scene;
        }


    }
}

