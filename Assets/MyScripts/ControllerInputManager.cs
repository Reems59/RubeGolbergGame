using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    //teleporter
    private LineRenderer laser;
    public GameObject teleporteAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;

    //aimer position
    public float yNudgeAmout = 1f;

    //dash
    public float dashSpeed = 0.1f;
    private bool isDashing = false;
    private float lerpTime;
    private Vector3 dashStartPosition;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();
    }

	
	// Update is called once per frame
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (isDashing)
        {
            lerpTime += Time.deltaTime * dashSpeed;
            player.transform.position = Vector3.Lerp(dashStartPosition, teleportLocation, lerpTime);
            if (lerpTime >= 1)
            {
                isDashing = false;
                lerpTime = 0;
            }
        }
        else
        {
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                teleporteAimerObject.SetActive(true);

                laser.SetPosition(0, gameObject.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
                {
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                    teleporteAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmout, teleportLocation.z);
                }
                else
                {
                    teleportLocation = transform.position + transform.forward * 15;
                    RaycastHit groundRay;
                    if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                    {
                        teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);     
                    }
                    laser.SetPosition(1, transform.forward * 15 + transform.position);
                    //aimer position
                    teleporteAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmout, 0);
                }

            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(false);
                teleporteAimerObject.SetActive(false);
                //player.transform.position = teleportLocation;

                dashStartPosition = player.transform.position;
                isDashing = true;


            }
        }
    }
}
