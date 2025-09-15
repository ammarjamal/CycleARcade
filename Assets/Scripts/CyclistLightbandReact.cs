using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclistLightbandReact : MonoBehaviour
{

    private Transform bike;
    public float lightUpDistance;
    private float distance;
    public Material cyan;
    public Material litUp;
    // Start is called before the first frame update
    void Start()
    {
       bike =  GameObject.FindWithTag("bike").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       distance =  Vector3.Distance(gameObject.transform.position, bike.position);
       if(distance <= lightUpDistance){
           gameObject.GetComponent<MeshRenderer>().material = litUp;
       }else{
           gameObject.GetComponent<MeshRenderer>().material = cyan;
       }
    }
}
