using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject correct;
    void Start()
    {
        correct.SetActive(false);
    }

    public void OnCorrectAnswer()
    {
        correct.SetActive(true);
    }


}
