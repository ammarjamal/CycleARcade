using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSet : MonoBehaviour
{


    public void PrefsTrack( string value){
         PlayerPrefs.SetString("track", value);
    }

    public void PrefsEHMI(string value){
         PlayerPrefs.SetString("ehmi", value);
    }

    public void PrefsParticipant(string value){
         PlayerPrefs.SetString("participant", "P" + value);
    }

}
