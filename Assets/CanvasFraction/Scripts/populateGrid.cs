using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateGrid : MonoBehaviour {

    // Use this for initialization
    public GameObject prefab; // This is our prefab object that will be exposed in the inspector
    public int numberToCreate; // number of objects to create. Exposed in inspector
    void Start () {
        Populate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < numberToCreate; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(prefab, transform);

            // Randomize the color of our image
            newObj.GetComponent<TEXDraw>().text = newObj.GetComponent<TEXDraw>().text.Insert(0, Random.Range(1, 9).ToString());
        }

    }
}
