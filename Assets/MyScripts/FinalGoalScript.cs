using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGoalScript : MonoBehaviour {

    private DataStarsGame dsg;
    public string sceneLoading;

    public void Start()
    {
        dsg = GameObject.Find("DataStarsGameObject").GetComponent<DataStarsGame>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") && dsg.getCollectedStarts() == dsg.getNbStarsInGame())
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SteamVR_LoadLevel.Begin(sceneLoading);
        }
    }
}
