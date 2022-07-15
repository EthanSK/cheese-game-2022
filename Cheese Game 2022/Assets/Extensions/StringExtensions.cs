using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ETGgames.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizePathAsUrl(this string str)
        {
            return str.Replace(" ", "%20"); //on apple devices it doesn't work with space
        }

        public static int LayerNameToMask(this string str)
        {
            return LayerMask.GetMask(str);
        }


        public static string WithoutRichText(this string str)
        {
            return Regex.Replace(str, @"<[^>]*>", ""); //call-static ETGgames.Extensions.StringExtensions WithoutRichText "Press <color=#dbcf2f>F</color> to Pay Respects"
        }

        public static string WithoutNewlines(this string str, string replacement = null)
        {
            return Regex.Replace(str, @"\t|\n|\r", replacement ?? "");
        }



    }
}
