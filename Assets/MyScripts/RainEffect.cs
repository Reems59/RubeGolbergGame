using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour {

    private bool isProtected = false;
    public Vector3 forceRainVector;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") && !isProtected)
        {
            Debug.Log("Je glisse");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceRainVector * 90f);
        }
    }

    public void SetProtected(bool ispro)
    {
        isProtected = ispro;
    }

    public bool IsProtected()
    {
        return isProtected;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.name == "RainParticle")
        {
            isProtected = false;
        }
    }
}
