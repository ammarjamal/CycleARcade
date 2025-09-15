using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioEnd : MonoBehaviour
{
    public GameObject car;
    private GameObject questionnaire;


    private void OnTriggerEnter(Collider other) {
        car.SetActive(false);
        PlayerPrefs.SetString("scenario", "no_scenario");
        questionnaire =  FindInActiveObjectByTag("questionnaire");
        questionnaire.SetActive(true);
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
