using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour {

    public bool menuHide = false;
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public GameObject menu;
    // Use this for initialization
    void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuHide)
        {
            menu.SetActive( false);
            menuHide = true;
        }
    }
}
