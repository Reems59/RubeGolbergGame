using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    private Vector3 initialPosition;
    private DataStarsGame dsg;
    public Vector3 minimumVelocityOfBall;
    private Rigidbody rb;
    public bool isThrowed = false;

	// Use this for initialization
	void Start () {
        initialPosition = gameObject.transform.position;
        dsg = GameObject.Find("DataStarsGameObject").GetComponent<DataStarsGame>();
        rb = gameObject.GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            resetBall();
            
        }
    }
    public void resetBall()
    {
        gameObject.transform.position = initialPosition;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        dsg.resetCompteur();
        isThrowed = false;
        foreach (GameObject g in dsg.getListStars())
        {
            g.SetActive(true);
        }
    }

    public void Update()
    {

        if(Mathf.Approximately(rb.velocity.x, 0) 
            && Mathf.Approximately(rb.velocity.y, 0) && Mathf.Approximately(rb.velocity.z, 0) && isThrowed)
        {
            resetBall();
        }
    }
}
