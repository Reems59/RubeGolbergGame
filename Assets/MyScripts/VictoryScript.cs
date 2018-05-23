using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour {

    private DataStarsGame dsg;
    public GameObject canvas;
    public Sprite victorySprite;

    public void Start()
    {
        dsg = GameObject.Find("DataStarsGameObject").GetComponent<DataStarsGame>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") && dsg.getCollectedStarts() == dsg.getNbStarsInGame())
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            canvas.transform.GetChild(0).GetComponent<Image>().sprite = victorySprite;
            canvas.SetActive(true);
        }
    }
}
