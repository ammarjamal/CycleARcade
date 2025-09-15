using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEhmi: MonoBehaviour
{

    private Transform bike;
    public bool yielding;
    public float lightUpDistance = 25f;
    private float distance;

    public GameObject yieldingState;
    public GameObject aggressiveState;

    private void Start() {
        bike =  GameObject.FindWithTag("bike").transform;
    }

    void FixedUpdate()
    {
        distance =  Vector3.Distance(gameObject.transform.position, bike.position);
        if(distance <= lightUpDistance){
           if(yielding){
                yieldingState.SetActive(true);
                aggressiveState.SetActive(false);

           }else{
               yieldingState.SetActive(false);
               aggressiveState.SetActive(true);
           }
       }else{
           yieldingState.SetActive(false);
           aggressiveState.SetActive(false);
       }

    }
}
