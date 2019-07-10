using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AnimatorExtensions
{
    // Nicht unbedingt geeignet für Update, da wir alle Parameter durchschauen.
    // Da runtime keine Parameter hinzukommen, können wir das Ergebnis cachen.
    public static bool HasParameter(this Animator animator, string parameterName)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == parameterName)
                return true;
        }
        return false;
        
        // Alternative mit using System.Linq; <- potentiell etwas langsamer, dafür besser lesbar
        // return animator.parameters.Any(parameter => parameter.name == parameterName);
    }
}
