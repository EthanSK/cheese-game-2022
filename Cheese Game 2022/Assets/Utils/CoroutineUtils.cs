using UnityEngine;
using System.Collections;
using System;

namespace ETGgames.Utils
{
    public static class CoroutineUtils
    {
        public static IEnumerator WaitForFrames(int numFrames)
        {
            for (int i = 0; i < numFrames; i++)
            {
                yield return new WaitForEndOfFrame();
            }
        }

    }


}