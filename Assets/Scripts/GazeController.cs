using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public GameObject sphere;
    OVREyeGaze eyeGaze;

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
    }

    void Update()
    {
        if (eyeGaze == null) return;

        if (eyeGaze.EyeTrackingEnabled)
        {
            sphere.transform.rotation = eyeGaze.transform.rotation;
            //sphere.transform.position = eyeGaze.transform.position;
            //sphere.transform.position = new Vector3(sphere.transform.position.x, eyeGaze.transform.position.y - 0.1f, sphere.transform.position.z);
        }
    }
}
