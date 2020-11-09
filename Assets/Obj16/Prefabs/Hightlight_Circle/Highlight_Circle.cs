using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight_Circle : MonoBehaviour
{

    private void OnEnable()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Play", true);

        if(this.gameObject.tag == "HighlightApple")
        {
            StartCoroutine(highlight(5.0f));
        }
        else if(this.gameObject.tag == "HighlightApplePart")
        {
            StartCoroutine(highlight(8.1f));

        }
        else if(this.gameObject.tag == "Untagged")
        {
            StartCoroutine(highlight(2f));
        }
        else if (this.gameObject.tag == "Circle")
        {
            StartCoroutine(highlight(4.2f));
        }
    }

    IEnumerator highlight(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

}
