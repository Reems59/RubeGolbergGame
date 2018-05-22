using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaOnZone : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "RainParticle")
        {
            other.gameObject.transform.parent.GetChild(1).GetComponent<RainEffect>().SetProtected(true);
        }
    }
}
