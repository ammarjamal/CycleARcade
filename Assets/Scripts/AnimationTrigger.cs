using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{

    [Header ("Game Objects")]
    //[SerializeField] private GameObject questionnaireMenu;
    public GameObject car;
    Animator carAnimator;

    private GameObject brake;
    Animator brakeAnimator;

    private GameObject wheels;
    Animator wheelAnimator;

    public string scenario;


    private void Awake() {

        brake = car.transform.Find("Brake Lights").gameObject;
        wheels = car.transform.Find("wheels").gameObject;

        carAnimator = car.GetComponent<Animator>();
        brakeAnimator = brake.GetComponent<Animator>();
        wheelAnimator = wheels.GetComponent<Animator>();
    }

 /* public void Pause(){
        Time.timeScale = 0.0f;
        questionnaireMenu.SetActive(true);
    }

    public void ResumeButton(){
        Time.timeScale = 1.0f;
        questionnaireMenu.SetActive(false);
    }*/

    private void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
        car.SetActive(true);
        //if(gameObject.CompareTag("CarTrigger")){
        carAnimator.SetTrigger("oncar");
        brakeAnimator.SetTrigger("onbrake");
        wheelAnimator.SetTrigger("onwheel");

        PlayerPrefs.SetString("scenario", scenario);
        //}
        //if(gameObject.CompareTag("ScenarioEnd")){
         //   Pause();
        //}
    }
}
