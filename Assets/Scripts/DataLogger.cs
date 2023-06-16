using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    //public FloatVariable BlyncSensorSpeed;
    //public FloatVariable BlyncTurnAngle;
    public OVRCameraRig cameraRig;

    private string dateTime;
    private string participant;
    private string track;
    private string scenario;
    private string speed;
    private string turnAngle;
    private string deviation;
    private string x_position;
    private string cameraRotation;
    
    
    private string record;

    private void Start() {
        InvokeRepeating("LogData", 1.0f, 1.0f);
    }

     void LogData() {
            dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            participant =  PlayerPrefs.GetString("participant");
            track = PlayerPrefs.GetString("track");
            scenario = PlayerPrefs.GetString("scenario");
            //speed = BlyncSensorSpeed.value.ToString();
            //turnAngle = BlyncTurnAngle.value.ToString();
            deviation = "";
            x_position = "" + transform.position.x;
            cameraRotation = cameraRig.centerEyeAnchor.rotation.ToString();
            record = dateTime + "," + participant + "," + track + "," + scenario + "," + speed + "," + turnAngle + "," + deviation + "," + x_position + "," + cameraRotation;
            SaveToFile(record);
    }

    public void SaveToFile(string content)
    {
        // Use the CSV generation from before
        //var content = ToCSV();

        // The target file path e.g.

        var filepath = Application.persistentDataPath + "/behaviour.csv";
        using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
        FileMode.Append, FileAccess.Write)))
        {
            writer.WriteLine(content);
        }
    }
}
