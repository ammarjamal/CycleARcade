using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MobilePermission : MonoBehaviour
{
    void Start()
    {
#if PLATFORM_ANDROID
        StartCoroutine(AskForPermissions());
#endif
    }


#if UNITY_ANDROID
    private IEnumerator AskForPermissions()
    {

        List<bool> permissions = new List<bool>() { false, false};
        List<bool> permissionsAsked = new List<bool>() { false, false };
        List<Action> actions = new List<Action>()
    {
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.Microphone);
            if (!permissions[0] && !permissionsAsked[0])
            {
                Permission.RequestUserPermission(Permission.Microphone);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                permissionsAsked[1] = true;
                return;
            }
        })
    };
        for (int i = 0; i < permissionsAsked.Count;)
        {
            actions[i].Invoke();
            if (permissions[i])
            {
                ++i;
            }
            yield return new WaitForEndOfFrame();
        }

}
#endif

}
