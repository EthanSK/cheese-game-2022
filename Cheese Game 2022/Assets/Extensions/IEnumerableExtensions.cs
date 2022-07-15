using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ETGgames.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> source)
        {
            return source.Select(el => el.ToString()).ToList();
        }
        public static string ToReadableString<T>(this IEnumerable<T> source)
        {
            return source.Aggregate("", (acc, next) => $"{acc}, {next}");
        }

        public static void Add<T>(this Stack<T> stack, T value)
        {
            stack.Push(value);
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(enumerable.RandomIndex());
        }

        public static int RandomIndex<T>(this IEnumerable<T> enumerable)
        {
            int index = new System.Random().Next(0, enumerable.Count());
            return index;
        }



    }
}
