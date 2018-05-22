using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStarsGame : MonoBehaviour {

    private int nbStars;
    private List<GameObject> collectStarsGameObjects;
    private int compteur;
	// Use this for initialization
	void Start ()
    {
        collectStarsGameObjects = new List<GameObject>();
        foreach (GameObject g in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (g.GetComponent<CollectStarScript>() != null)
            {
                nbStars++;
                collectStarsGameObjects.Add(g);
            }
        }
        compteur = 0;
        Debug.Log(nbStars);
    }
	
    public void StarCollected()
    {
        compteur++;
    }

    public void resetCompteur()
    {
        compteur = 0;
    }

    public int getNbStarsInGame()
    {
        return nbStars;
    }

    public int getCollectedStarts()
    {
        return compteur;
    }

    public List<GameObject> getListStars()
    {
        return collectStarsGameObjects;
    }

}
