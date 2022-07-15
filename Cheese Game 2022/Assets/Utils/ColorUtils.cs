using UnityEngine;
using System.Collections;

namespace ETGgames.Utils
{
    public static class ColorUtils
    {
        public static Color RandomBrightColorfulColor()
        {
            return UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        }

        public static Color RandomMediumBrightColorfulColor()
        {
            return UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.3f, 0.6f);
        }

    }


}