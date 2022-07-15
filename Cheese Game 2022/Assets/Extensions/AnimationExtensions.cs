using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class AnimationExtensions
    {

        public static bool ContainsParam(this Animator animator, string paramName)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.name == paramName) return true;
            }
            return false;
        }

    }
}

