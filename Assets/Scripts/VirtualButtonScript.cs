using System;
using System.Collections;
using UnityEngine;
using Vuforia;

public class VirtualButtonScript : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject virtualButtonObject;

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("virtual button pressed");
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("virtual button released");
    }

    // Use this for initialization
    void Start ()
    {
        virtualButtonObject = GameObject.Find("showInfoButton");
        if (virtualButtonObject == null)
            Debug.Log("vb not found");

        virtualButtonObject.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler(this);
    }
	
	
}
