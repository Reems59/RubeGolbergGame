using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStarScript : MonoBehaviour {

    private DataStarsGame dsg;

    public void Start()
    {
        dsg = GameObject.Find("DataStarsGameObject").GetComponent<DataStarsGame>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Throwable") && collision.gameObject.GetComponent<BallReset>().isThrowed)
        {
            gameObject.SetActive(false);
            dsg.StarCollected();
        }
    }

    public void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 150f, 0));
    }
}
