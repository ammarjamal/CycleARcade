using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.5f;

    void OnEnable()
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
