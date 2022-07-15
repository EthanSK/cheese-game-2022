using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public enum MonthMemeItem
{
    January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
}


// we still typically need to either cast this to a DateTime or read the DateTime field directly
[System.Serializable] //this makes the class non nullable. essentially treated as a struct
public class DateMemeItemInspector : ISerializationCallbackReceiver
{

    [HideInInspector] public DateTimeOffset DateTime;

    //having the day is actually so useless. We don't show it or use it anywhere, it's probably gonna be super innacurate, and a waste of time and space.
    [SerializeField] private MonthMemeItem _month; //default value won't set itself reee. it works if we set it in OnAfterDeserialize

    [Range(2004, 2022)][SerializeField] private int _year; //this doesn't fucking work. works if we set it in OnAfterDeserialize


    public static implicit operator DateTimeOffset(DateMemeItemInspector dtmii)
    {
        return (dtmii.DateTime);
    }

    public static implicit operator DateMemeItemInspector(DateTimeOffset dt)
    {
        return new DateMemeItemInspector() { DateTime = dt };
    }

    public void OnAfterDeserialize()
    {
        DateTime = new DateTime(
            year: Math.Max(Config.LevelBuilder.MinYearMemeItem, _year), //because it defaults to 0
            month: Math.Max((int)_month, 1), //same problem with default value not setting here
            day: 1 //middle of the month so no timezone ambiguity //actually, timezone won't matter here coz its fixed data within the game. we want it to be the 1st so it works with the filters in the lvl builder
        ).ToUniversalTime(); //use datetime coz DateTimeOffset doens't have a constructor that takes these
    }

    public void OnBeforeSerialize()
    {
        _month = (MonthMemeItem)DateTime.Month;
        _year = Math.Min(Config.LevelBuilder.MaxYearMemeItem, DateTime.Year);
    }

}
