using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Utility class, used for custom code that is used in multiple places
public static class Utility
{
    public static IEnumerator wait(float waitTime) // simple code that can be used in multiple classes to wait for a moment.
    {
        float cnt = 0;
        while (cnt < waitTime)
        {
            cnt += Time.deltaTime;
            yield return null;
        }
    }

}
