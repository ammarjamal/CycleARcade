using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BikeMover : MonoBehaviour
{
    public Transform front;
    public Transform bike;
    public Transform pedals;
    public Transform frontWheel;
    public Transform backWheel;
    

    private Quaternion originalFrontRot;
    private GameObject gameController;
    public float turnAngle = 0.0f;
    public float speed = 0.0f;
    private float movementZ;
    private float rotationX;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        originalFrontRot = front.transform.localRotation;
    }

    private void FixedUpdate() {
        //OVRInput.Update();
        OVRInput.FixedUpdate();
        speed = gameController.GetComponent<BLE>().speed;
        //moveForward(speed);
        setTurnAngle();
        checkRotation();
        //bike.transform.rotation = Quaternion.Euler(0, bike.transform.rotation.y, 0);
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0)
        {
            bike.transform.position = new Vector3(0, bike.transform.position.y, bike.transform.position.z);
            bike.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //moveForward();
        
    }

     void setTurnAngle(){
        turnAngle = (OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch)).y * 100;
    }

    void checkRotation(){
        front.transform.localRotation = originalFrontRot * Quaternion.Euler(0, turnAngle, 0);
        if (speed > 0f)
        {
            bike.transform.Rotate(0, Time.deltaTime * (turnAngle), 0, Space.Self);
        }
    }

    void moveForward(float speed){
        movementZ = 0.0f;
        rotationX = 0.0f;
        if (speed > 0.0000f)
        //if(Input.GetAxis("Horizontal") > 0.0f)
        {
            movementZ = speed;
            rotationX = 20f;
        }


        Vector3 movement = new Vector3(0.0f, 0.0f, speed);
        Vector3 rotation = new Vector3(rotationX, 0.0f, 0.0f);

        bike.transform.Translate(movement * Time.deltaTime);
        frontWheel.Rotate(rotation * speed * Time.deltaTime);
        backWheel.Rotate(rotation * speed * Time.deltaTime);
        pedals.Rotate(rotation * (speed) * Time.deltaTime);
    }
}
