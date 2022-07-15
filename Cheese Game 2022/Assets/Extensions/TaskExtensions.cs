
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class TaskExtensions
    {
        public async static void WithTryCatchWrapper(this Task task)
        {
            try
            {
                await task;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                // throw e; //for some reason this gets caught properly and shows in the console. So if we log and then throw here, it will show in console twice
            }
        }
    }
}
