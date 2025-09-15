using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnd : MonoBehaviour
{
    private GameObject startMenu;
    private GameObject bike;

    public List<GameObject> tracks;

    private void Start() {
        startMenu = FindInActiveObjectByTag("start");
        bike = FindInActiveObjectByTag("bike");
    }
    
    private void OnTriggerEnter(Collider other) {

        foreach (var track in tracks) {
            track.SetActive(false);
    }
        startMenu.SetActive(true);
        bike.transform.position = new Vector3(-390f, -0.9f, 1f);
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
