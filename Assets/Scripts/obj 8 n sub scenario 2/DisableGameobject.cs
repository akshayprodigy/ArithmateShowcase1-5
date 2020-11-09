using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameobject : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable()
    {
     //this.GetComponent<Animator>().SetBool("Play", false);

    }

    void OnEnable()
    {
        //this.GetComponent<Animator>().SetBool("Play", true);
        StartCoroutine(disable_pnl());

    }

    public IEnumerator disable_pnl()
    {
        if (this.gameObject.tag == "ErrorMsg")
        {
            yield return new WaitForSeconds(5.0f);
            this.gameObject.SetActive(false);
            Debug.Log("Highlight gone");
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
            this.gameObject.SetActive(false);
            Debug.Log("Highlight gone");
        }

    }
}
