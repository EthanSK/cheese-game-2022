using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ETGgames.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<TEnum> ToEnumList<TEnum>(this IEnumerable<string> stringList) where TEnum : struct
        {
            foreach (string item in stringList)
            {
                TEnum parseRes;
                if (Enum.TryParse(item, ignoreCase: false, out parseRes)) yield return parseRes;
            }
        }

        public static TEnum ToEnum<TEnum>(this string text) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }



    }
}
