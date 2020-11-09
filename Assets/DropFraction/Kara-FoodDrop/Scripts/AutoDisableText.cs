using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableText : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("off", 2.0f);
    }
    void off()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void OnDisable()
    {
        if (GameObject.FindObjectOfType<GameManager1>().s1count == 1)
        {
            foreach (GameObject g in GameObject.FindObjectOfType<GameManager1>().s1)
                g.SetActive(false);
            GameObject.FindObjectOfType<GameManager1>().s1[GameObject.FindObjectOfType<GameManager1>().userTotalCorrect].SetActive(true);

        }
        else if (GameObject.FindObjectOfType<GameManager1>().s1count == 2)
        {
            foreach (GameObject g1 in GameObject.FindObjectOfType<GameManager1>().s2)
                g1.SetActive(false);
            GameObject.FindObjectOfType<GameManager1>().s2[GameObject.FindObjectOfType<GameManager1>().userTotalCorrect].SetActive(true);

        }
    }
}
