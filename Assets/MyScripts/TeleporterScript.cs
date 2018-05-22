using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour {

    public GameObject portal2;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Throwable"))
        {
            ContactPoint contact = collision.contacts[0];
            Debug.Log(contact.point);
            Vector3 tmpPos = collision.gameObject.transform.position;
            collision.gameObject.transform.position = portal2.gameObject.transform.position;
        }
    }
}
