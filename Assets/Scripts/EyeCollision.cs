using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class EyeCollision : MonoBehaviour
{
    float timer = 0.0f;
    //public TextMeshPro secondsText;
    bool lookedAt = false;
    //double displayTime = 0.0;
    private string aoi = "";

   private void Update(){
       if(lookedAt){
           timer += Time.deltaTime;
           //displayTime = Math.Round(timer % 60, 1);
           //secondsText.text = ""+displayTime;
       }

   } 
  
   private void OnTriggerEnter(Collider other){

      if (other.gameObject.CompareTag("aoi")) 
        {
            lookedAt = true;
        }
   }

   private void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("aoi")){
            lookedAt = false;
            //DateTime.Now;
            //PlayerPrefs.GetString("participant");
            //write to csv: aoi = timestamp, scenario, track, ehmi, participant no., gameobject.name, time looked at
            aoi = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "," + PlayerPrefs.GetString("participant") + "," + PlayerPrefs.GetString("track") + "," + PlayerPrefs.GetString("scenario") + "," + PlayerPrefs.GetString("ehmi") + "," + other.gameObject.name + "," + timer.ToString(); 
            SaveToFile(aoi);
        }
   }

   public void SaveToFile(string content)
    {
        // Use the CSV generation from before
        //var content = ToCSV();

        // The target file path e.g.

        var filepath = Application.persistentDataPath + "/eyeTracking.csv";
        using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
        FileMode.Append, FileAccess.Write)))
        {
            writer.WriteLine(content);
        }
    }
}
