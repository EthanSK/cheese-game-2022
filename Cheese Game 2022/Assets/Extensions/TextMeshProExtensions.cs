using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class TextMeshProExtensions
    {

        //https://forum.unity.com/threads/textmesh-pro-change-bold-underline-at-runtime.515623/
        public static T AddFontStyle<T>(this T tmp, FontStyles fontStyle) where T : TMP_Text
        {
            tmp.fontStyle |= fontStyle;
            return tmp;
        }

        public static T RemoveFontStyle<T>(this T tmp, FontStyles fontStyle) where T : TMP_Text
        {
            tmp.fontStyle &= ~fontStyle;//https://forum.unity.com/threads/how-to-remove-underline-from-tmp_text-from-script.535337/
            return tmp;
        }

        public static T SetFontStyleEnabled<T>(this T tmp, FontStyles fontStyle, bool isEnabled) where T : TMP_Text
        {
            if (isEnabled) tmp.AddFontStyle(fontStyle);
            else tmp.RemoveFontStyle(fontStyle);
            return tmp;
        }


        public static bool HasFontStyle(this TMP_Text tmp, FontStyles fontStyle)
        {
            return (tmp.fontStyle & fontStyle) != 0;
        }


    }
}
