using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FairSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dropoffMenu;



    private void Start()
    {
        HideDropOffMenu();
    }

    public void GoToLoadingPage()
    {
        if (dropoffMenu.gameObject.active)
            HideDropOffMenu();

        Debug.Log("(UtilityArtifacts.NJMsg: " + UtilityArtifacts.NJMsg.user_data.asset_bundle_address);
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(UtilityArtifacts.NJMsg, "true");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }

    public void onNormalGameOver()
    {
        if (dropoffMenu.gameObject.active)
            HideDropOffMenu();
        // print outputmessgae
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 2;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);

    }

    public void showDropOffMenu()
    {
        dropoffMenu.gameObject.SetActive(true);
    }

    public void HideDropOffMenu()
    {
        dropoffMenu.gameObject.SetActive(false);
    }
}
