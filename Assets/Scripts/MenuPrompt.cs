using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrompt : MonoBehaviour
{
    private GameObject raycast;

    private void Awake() {
        raycast =  FindInActiveObjectByTag("ray");
    }

    private void OnEnable() {
        raycast.SetActive(true);
        Time.timeScale = 0.0f;
    }

    //private void OnDisable() {
     //   raycast.SetActive(false);
      //  Time.timeScale = 1.0f;
    //}

    public void PlayGame(){
        raycast.SetActive(false);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

     GameObject FindInActiveObjectByTag(string tag)
{

    Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    for (int i = 0; i < objs.Length; i++)
    {
        if (objs[i].hideFlags == HideFlags.None)
        {
                if (objs[i].CompareTag(tag))
                {
                return objs[i].gameObject;
                }
         }
        }
        return null;
    }
}
