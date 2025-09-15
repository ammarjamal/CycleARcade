using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject car;
    Animator carAnimator;

    private GameObject brake;
    Animator brakeAnimator;

    private GameObject wheels;
    Animator wheelAnimator;

    [Header("Configuration")]
    public string scenario;
    public bool yielding = true;

    private void Awake()
    {
        brake = car.transform.Find("Brake Lights").gameObject;
        wheels = car.transform.Find("wheels").gameObject;

        carAnimator = car.GetComponent<Animator>();
        brakeAnimator = brake.GetComponent<Animator>();
        wheelAnimator = wheels.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bike"))
        {
            // Set scenario and yielding in global variables
            GlobalVariables.Scenario = scenario;
            GlobalVariables.Yielding = yielding;

            // Deactivate current object
            gameObject.SetActive(false);

            // Activate car and play animations
            car.SetActive(true);
            carAnimator.SetTrigger("oncar");
            brakeAnimator.SetTrigger("onbrake");
            wheelAnimator.SetTrigger("onwheel");
        }
    }
}

/*using UnityEngine;
using Fusion;
using TMPro;  // Include the TextMesh Pro namespace

public class AnimationTrigger : NetworkBehaviour
{
    [Header("Game Objects")]
    public GameObject car;
    private Animator carAnimator;

    private GameObject brake;
    private Animator brakeAnimator;

    private GameObject wheels;
    private Animator wheelAnimator;

    [Header("UI Components")]
    public TextMeshPro debugText;  // Reference to the TextMesh Pro component for outputting logs

    [Header("Configuration")]
    public string scenario;
    public bool yielding = true;

    private void Awake()
    {
        brake = car.transform.Find("Brake Lights").gameObject;
        wheels = car.transform.Find("wheels").gameObject;

        carAnimator = car.GetComponent<Animator>();
        brakeAnimator = brake.GetComponent<Animator>();
        wheelAnimator = wheels.GetComponent<Animator>();

        if (!debugText)
        {
            Debug.LogError("TextMeshPro component not assigned.", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bike"))
        {
            UpdateText("Bike collided, triggering RPC.");
            TriggerAnimationsRPC();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void TriggerAnimationsRPC()
    {
        UpdateText("RPC triggered: Activating car and animations.");
        gameObject.SetActive(false);
        car.SetActive(true);
        carAnimator.SetTrigger("oncar");
        brakeAnimator.SetTrigger("onbrake");
        wheelAnimator.SetTrigger("onwheel");
    }

    // Helper method to update the text on TextMesh Pro
    private void UpdateText(string message)
    {
        if (debugText != null)
            debugText.text = message;
        else
            Debug.LogError("TextMesh Pro component is not assigned.", this);
    }
}*/






