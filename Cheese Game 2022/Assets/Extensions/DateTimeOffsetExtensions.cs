using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ETGgames.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static string ToHumanReadableString(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToLocalTime().ToString("D");
        }
    }
}

namespace ETGgames.CheeseGame.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToHumanReadableString(this TimeSpan timeSpan)
        {
            var mins = timeSpan.TotalMinutes;
            return $"{(int)mins}:{timeSpan.Seconds.ToString("00")}:{timeSpan.Milliseconds.ToString("000")}";
        }

    }
}