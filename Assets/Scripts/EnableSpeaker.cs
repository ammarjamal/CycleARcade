using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpeaker : MonoBehaviour
{

    public GameObject speaker;
    private Transform bike;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        bike =  GameObject.FindWithTag("bike").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance =  Vector3.Distance(gameObject.transform.position, bike.position);
       if(distance <= 40.0f){
           speaker.SetActive(true);
       }
    }
}
