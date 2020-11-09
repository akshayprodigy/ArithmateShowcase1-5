using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlashCardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text MainText,loading;
    int sceneNumber = 8;
    public GameObject flashcard_multi, flashcard_div,next_button;
    public AudioClip[] multiplication_audio, division_audio;
    public AudioSource flash_audio_source;
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    void Start()
    {

        //Debug.LogError("Prerequisit: flash " + UtilityREST.prerequiste);
        loading.gameObject.SetActive(false);
        if (string.Equals(UtilityREST.prerequiste, UtilityArtifacts.multiplicationId))
        {
            // show multiplication
            UtilityArtifacts.scafoldCanvas = true;
            UtilityArtifacts.isMultipication = true;
            MainText.text = "Show Flash card here";
            show_multiplication_flashcard();
        }
        else if (string.Equals(UtilityREST.prerequiste, UtilityArtifacts.divisionId))
        {
            // show division
            UtilityArtifacts.scafoldCanvas = true;
            MainText.text = "Show Flash card here";
            show_division_flashcard();
        }
        else
        {
            UtilityArtifacts.scafoldCanvas = false;
            MainText.gameObject.SetActive(true);
            loading.gameObject.SetActive(true);
            MainText.text = "Let’s play a game to test what we have learnt";//"lets Go Active Experiment";
            loading.text = "Loading....   ";
            sceneNumber = 7;
            Invoke("LoadScene", 3);
            if (onLogMessage != null)
                onLogMessage("'Active Experimentation’ Session begins");
        }
            
    }

    void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void nextButtonClicked()
    {
        
        if (UtilityArtifacts.scafoldCanvas)
        {
            sceneNumber = UtilityArtifacts.CanvasSceneNumber;
        }
        else
        {
            sceneNumber = 0;//7// go to home page for reloading
            UtilityArtifacts.currentScene = 7;
        }

        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber);

        // Wait until the asynchronous scene fully loads
        // UtilityArtifacts.current_json = UtilityArtifacts.json_story_board;
        while (!asyncLoad.isDone)
        {

            yield return null;
        }


    }



    public void show_division_flashcard()
    {
        StartCoroutine(division_flash_numerator());
    }
    IEnumerator division_flash_numerator()
    {
        flashcard_div.SetActive(true);
        next_button.SetActive(false);
        for (int i = 0; i < division_audio.Length; i++)
        {
            flash_audio_source.clip = division_audio[i];
            flash_audio_source.time = 0;
            flash_audio_source.Play();
            yield return new WaitForSeconds(division_audio[i].length);
            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForSeconds(3f);
        
        next_button.SetActive(true);
    }

    public void show_multiplication_flashcard()
    {
        StartCoroutine(multiplication_flash_numerator());
    }

    IEnumerator multiplication_flash_numerator()
    {
        Debug.Log("multiplication_flash_numerator");
        flashcard_multi.SetActive(true);
        next_button.SetActive(false);
        for (int i = 0; i < multiplication_audio.Length; i++)
        {
            flash_audio_source.clip = multiplication_audio[i];
            flash_audio_source.time = 0;
            flash_audio_source.Play();
            yield return new WaitForSeconds(multiplication_audio[i].length);
            yield return new WaitForSeconds(0.5f);

        }

        //yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("multiplication_flash_numerator");
        if (onLogMessage != null)
            onLogMessage("Validating multiplication learning through problems");
        next_button.SetActive(true);
    }
}
