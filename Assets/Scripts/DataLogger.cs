using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public OVRCameraRig cameraRig;

    private string dateTime;
    private string participant;
    private string track;
    private string scenario;
    private float speed = 0.0f;
    private float turnAngle = 0.0f;
    private float deviation;
    private float z_position;
    private float cameraRotation;
    private string eHMI;
    private GameObject bike;

    
    
    private string record;

    private void Start() {
        bike = GameObject.FindWithTag("bike");
        
        InvokeRepeating("LogData", 1.0f, 1.0f);
    }

     void LogData() {
            dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            participant =  PlayerPrefs.GetString("participant");
            track = PlayerPrefs.GetString("track");
            scenario = PlayerPrefs.GetString("scenario");
            eHMI = PlayerPrefs.GetString("ehmi");
            speed = bike.GetComponent<BLE>().speed;
            turnAngle = bike.GetComponent<BLE>().turnAngle;
            deviation = bike.transform.position.x;
            z_position = bike.transform.position.z;
            cameraRotation = transform.rotation.y;
            record = $"{dateTime},{participant},{track},{scenario},{eHMI},{speed},{turnAngle},{deviation},{z_position},{cameraRotation}";
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
