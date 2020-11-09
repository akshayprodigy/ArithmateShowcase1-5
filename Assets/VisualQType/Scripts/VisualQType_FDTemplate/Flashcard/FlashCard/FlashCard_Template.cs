using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashCard_Template : MonoBehaviour
{

    GameObject flashCard;
    Button okayBtn;

    private void Awake()
    {
        flashCard = GameObject.Find("FlashCard_Template/Flash_Card").gameObject;
        okayBtn = GameObject.Find("FlashCard_Template/Flash_Card/Button_Ok").GetComponent<Button>();
        flashCard.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        flashCard.SetActive(true);
        flashCard.GetComponent<Animator>().SetBool("Play", true);
        okayBtn.onClick.AddListener(() => OnOkayBtn());

    }

    void OnOkayBtn()
    {

        flashCard.GetComponent<Animator>().SetBool("Play", false);
        flashCard.SetActive(false);


        //StartCoroutine(StopAnimation(0.5f));

    }

    IEnumerator StopAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        flashCard.GetComponent<Animator>().SetBool("Play", false);

    }
}
