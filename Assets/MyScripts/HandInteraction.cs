using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public float throwForce = 1.5f;
    private bool isOpen = false;
    public GameObject menuInstructions;
    public bool menuHide = false;
    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchLastY;
    public float touchCurrent;
    public float touchCurrentY;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public ObjectMenuManager objectMenuManager;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuHide)
        {
            menuInstructions.SetActive(false);
            menuHide = true;
        }else
        {
            if (objectMenuManager != null)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuHide)
                {
                    if (isOpen)
                    {
                        objectMenuManager.HideMenu();
                        isOpen = false;
                    }
                    else
                    {
                        objectMenuManager.DisplayMenu();
                        isOpen = true;
                    }
                }
                if (objectMenuManager.gameObject.activeInHierarchy)
                {
                    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                        touchLastY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;
                    }
                    if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                    {

                        touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                        touchCurrentY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;
                        distance = touchCurrent - touchLast;
                        touchLast = touchCurrent;
                        swipeSum += distance;
                        if (!hasSwipedRight)
                        {
                            if (touchCurrent > 0f && touchCurrentY > -0.2f)
                            {
                                swipeSum = 0;
                                SwipeRight();
                                hasSwipedRight = true;
                                hasSwipedLeft = false;
                            }
                        }
                        if (!hasSwipedLeft)
                        {

                            if (touchCurrent < 0f && touchCurrentY > -0.2f)
                            {
                                swipeSum = 0;
                                SwipeLeft();
                                hasSwipedLeft = true;
                                hasSwipedRight = false;
                            }
                        }
                    }
                    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        swipeSum = 0;
                        touchCurrent = 0;
                        touchLast = 0;
                        hasSwipedLeft = false;
                        hasSwipedRight = false;
                    }
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        if (touchCurrentY < 0f)
                        {
                            SpawnObject();
                        }
                        //Spawn object currently selected by menu
                    }
                }
            }
        }
        
    }

    void SpawnObject()
    {
        objectMenuManager.SpawnCurrentObject();
    }

    void SwipeLeft()
    {
        objectMenuManager.MenuLeft();
    }
    void SwipeRight()
    {
        objectMenuManager.MenuRight();
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable") || col.gameObject.CompareTag("Structure"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(col);
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {              
                GrabObject(col);
            }
        }
    }
    void GrabObject(Collider coli)
    {
        BallReset br = coli.GetComponent<BallReset>();
        if (br != null && br.isThrowed == true)
        {
            Debug.Log("Can't take it");
        }else
        {
            coli.transform.SetParent(gameObject.transform);
            coli.GetComponent<Rigidbody>().isKinematic = true;
            device.TriggerHapticPulse(2000);
        }
        

    }
    void ThrowObject(Collider coli)
    {
        coli.transform.SetParent(null);
        Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
        if (!coli.gameObject.CompareTag("Structure"))
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity = device.velocity * throwForce;
            rigidBody.angularVelocity = device.angularVelocity;
            BallReset br = coli.GetComponent<BallReset>();
            if (br != null)
            {
                br.isThrowed = true;
            }
        }
    }

}
