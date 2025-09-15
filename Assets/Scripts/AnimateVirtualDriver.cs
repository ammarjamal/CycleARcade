using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class AnimateVirtualDriver : MonoBehaviour
{

    private Transform bike;
    public bool yielding;
    public float lightUpDistance = 30f;
    private float distance;


    public VideoClip driverYielding;
    public VideoClip driverMoving;
    
    private VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        bike =  GameObject.FindWithTag("bike").transform;
        player = gameObject.GetComponent<VideoPlayer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance =  Vector3.Distance(gameObject.transform.position, bike.position);
        if(distance <= lightUpDistance){
           if(yielding){
                    player.clip = driverYielding;
                    player.Play();
                    }
           else{
                    player.clip = driverMoving;
                    player.Play();
       }
    }
       
       else{
                    player.Stop();
       }
    }
}
