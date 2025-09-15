using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableEHMI : MonoBehaviour
{

    private string selectedEHMI;
    private List<GameObject> eHMIs;
    private void OnEnable() {
            selectedEHMI = PlayerPrefs.GetString("ehmi");
        foreach (Transform child in transform){
            if(child.gameObject.CompareTag(selectedEHMI)){
                child.gameObject.SetActive(true);
            }else{
                child.gameObject.SetActive(false);
            }
        }
    }
}
