using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour
{
    public List<GameObject> objectList; //handled automatically at start
    public List<GameObject> objectPrefabList; //set manually in inspector and MUST match order
                                              //of scene menu objects
    public int[] nbInstanceObjects;
    public int currentObject = 0;
    private Text[] textFieldObject;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
        textFieldObject = new Text[objectPrefabList.Count];
        int i = 0;
        foreach(Transform child in transform)
        {
            Debug.Log(child.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).name);
            textFieldObject[i] = child.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
            Debug.Log(nbInstanceObjects[i].ToString());
            textFieldObject[i].text = nbInstanceObjects[i].ToString();
            i++;
        }
    }

    public void MenuLeft()
    {
        Debug.Log("left");
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }
    public void MenuRight()
    {
        Debug.Log("Right");
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }
    public void SpawnCurrentObject()
    {
        if(nbInstanceObjects.Length > 0)
        {
            if (nbInstanceObjects[currentObject] > 0)
            {
                Instantiate(objectPrefabList[currentObject],
                objectList[currentObject].transform.position,
                objectList[currentObject].transform.rotation);
                nbInstanceObjects[currentObject]--;
                textFieldObject[currentObject].text = nbInstanceObjects[currentObject].ToString();
            }
        }
        
        
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void DisplayMenu()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
