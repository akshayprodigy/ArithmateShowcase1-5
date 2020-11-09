using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
    public static FortuneWheelManager Instance;
    private bool _isStarted,_startSpinning;
    public float[] _sectorsAngles;
    public float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Circle; 			// Rotatable Object with rewards
    //public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    //public Text CurrentCoinsText; 		// Pop-up text with wasted or rewarded coins amount
   // public int TurnCost = 300;			// How much coins user waste when turn whe wheel
    //public int CurrentCoinsAmount = 1000;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    public int PreviousCoinsAmount;		// For wasted coins animation
    public int interationCount = 12;
    public List<float> _previousFinalAngle;
    
    public delegate void SpinComplete(string value);
    public static event SpinComplete OnSpinComplete;

    private void Awake ()
    {
        //PreviousCoinsAmount = CurrentCoinsAmount;
        //CurrentCoinsText.text = CurrentCoinsAmount.ToString ();
        Instance = this;
        instintiate();
    }
    public void instintiate()
    {
        setAngel();
    }
    public void setAngel()
    {
        if (UtilityArtifacts.backTraversal)
          _startAngle=  PlayerPrefs.GetFloat("LastFraction_Value");
    }
    private void Start()
    {
        //Invoke("StatSpinning", 0.5f);
    }

    public void StatSpinning()
    {
        _currentLerpRotationTime = 0f;

        // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
        _sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };

        int fullCircles = 5;
        float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);

        _startSpinning = true;
        _isStarted = true;
    }

    public void TurnWheel () //Gacha Button Function
    {
        // Player has enough money to turn the wheel
        //if (CurrentCoinsAmount >= TurnCost) {
        _currentLerpRotationTime = 0f;

        // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
        _sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };

        int fullCircles = 5;
        float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);

        _isStarted = true;
        //_startSpinning = false;

        //if (_previousFinalAngle.Count > _sectorsAngles.Length)

        if (_previousFinalAngle.Count > interationCount)
        {
            _previousFinalAngle.Clear();
            //_previousFinalAngle.RemoveAt(0);
            //_previousFinalAngle.RemoveAt(1);

        }
        _previousFinalAngle.Add(_finalAngle);

        if (_previousFinalAngle.Count > 1 && _previousFinalAngle.Count <= interationCount)
        {

            for (int i = 0; i < (_previousFinalAngle.Count-1); i++)
            {
                if(_previousFinalAngle[i] == _finalAngle)
                {
                    interationCount++;
                    TurnWheel();
                }

            }

        }

        StartCoroutine(checkAngles());
        

        //_currentLerpRotationTime = 2f;
        //PreviousCoinsAmount = CurrentCoinsAmount;

        // Decrease money for the turn
        //CurrentCoinsAmount -= TurnCost;

        // Show wasted coins
        //CoinsDeltaText.text = "-" + TurnCost;
        //CoinsDeltaText.gameObject.SetActive (true);

        // Animate coins
        StartCoroutine(HideCoinsDelta ());
    	  //  StartCoroutine (UpdateCoinsAmount ());
    	//}
    }

    IEnumerator checkAngles()
    {
        yield return new WaitForSeconds(0.3f);
        
    }

    private void GiveAwardByAngle ()
    {
    	// Here you can set up rewards for every sector of wheel

    	switch ((int)_startAngle) {
            //case 0:
            //    RewardCoins ("Fifteen twentieths");
            //    break;
            //case -330:
            //    RewardCoins ("One third");
            //    break;
            //case -300:
            //    RewardCoins ("Half");
            //    break;
            //case -270:
            //    RewardCoins ("Four fifteenths");
            //    break;
            //case -240:
            //    RewardCoins ("One fourth");
            //    break;
            //case -210:
            //    RewardCoins ("Half");
            //    break;
            //case -180:
            //    RewardCoins ("Five fifteenths");
            //    break;
            //case -150:
            //    RewardCoins ("One third");
            //    break;
            //case -120:
            //    RewardCoins ("Half");
            //    break;
            //case -90:
            //    RewardCoins ("Five fifteenths");
            //    break;
            //case -60:
            //    RewardCoins ("One fourth");
            //    break;
            //case -30:
            //    RewardCoins ("Half");
            //    break;
            //default:
            //    RewardCoins ("One fourth");
            //    break;
            case 0:
                RewardCoins("Six eighth");
                break;
            case -330:
                RewardCoins("Half");
                break;
            case -300:
                RewardCoins("One third");
                break;
            case -270:
                RewardCoins("Two third");
                break;
            case -240:
                RewardCoins("Three fourth");
                break;
            case -210:
                RewardCoins("One fourth");
                break;
            case -180:
                RewardCoins("Five Eleventh");
                break;
            case -150:
                RewardCoins("Three tenth");
                break;
            case -120:
                RewardCoins("Seven eighth");
                break;
            case -90:
                RewardCoins("Four sixth");
                break;
            case -60:
                RewardCoins("Eight thirteenths");
                break;
            case -30:
                RewardCoins("Four fifth");
                break;
            default:
                RewardCoins("One fourth");
                break;
        }
    }

    public void DisableSpinButton()
    {
        TurnButton.interactable = false;
        TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
    }

    public void EnableSpinButton()
    {
        TurnButton.interactable = true;
        TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    }

    void LoadScene_Obj7()
    {
        //Show the traversal panel to obj7
        FindObjectOfType<Obj7spinwheeltalkCanvasManager>().Show_TraverseToObj7_Panel();
    }


    void Update ()
    {
        // Make turn button non interactable if user has not enough money for the turn && !_startSpinning
        if (_isStarted) {
            DisableSpinButton(); //Test

            //PlayerPrefs.SetFloat("LastFraction_Value", _finalAngle);
            //Invoke("LoadScene_Obj7", 2);

        } else {
    	    
    	}

            //Circle.transform.eulerAngles = new Vector3(0, 0, Circle.transform.eulerAngles.z-10.0f);
        
    	if (!_isStarted)
    	    return;

    	float maxLerpRotationTime = 4f;

        //if (_startSpinning)
        //{
        //    maxLerpRotationTime += Time.deltaTime;
        //}
            // increment timer once per frame
            _currentLerpRotationTime += Time.deltaTime;
        //if (_startSpinning && _currentLerpRotationTime > 2)
        //{
        //    _currentLerpRotationTime = 2;
        //}
            if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
    	    _currentLerpRotationTime = maxLerpRotationTime;
    	    _isStarted = false;
    	    _startAngle = _finalAngle % 360;
    
    	    GiveAwardByAngle ();
    	    StartCoroutine(HideCoinsDelta ());
    	}
    
    	// Calculate current position using linear interpolation
    	float t = _currentLerpRotationTime / maxLerpRotationTime;
        //Debug.Log("t: " + t);
        //if (t < 0.5f)
        //    t = 0.5f;
        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);
    
    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
        //Debug.Log("t: " + t+ "angle: "+ angle+ " _startAngle: " + _startAngle+ "_finalAngle: "+ _finalAngle);
        Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
        if (_startSpinning)
        {
           
            //if(angle%360 > prevAngle % 360)
            //{
            //    _finalAngle -= 360;
            //    Debug.Log(angle + "angle: _finalAngle: " + _finalAngle+ " t: "+ t+ " _currentLerpRotationTime: "+ _currentLerpRotationTime);
            //}
          
        }
        prevAngle = angle;
    }
    float prevAngle = 0;
    private void RewardCoins (string awardCoins)
    {
        if (OnSpinComplete != null)
            OnSpinComplete(awardCoins);
       // CurrentCoinsAmount += awardCoins;
       //CoinsDeltaText.text = "+" + awardCoins;
       //CoinsDeltaText.gameObject.SetActive (true);
        StartCoroutine (UpdateCoinsAmount ());
    }

    private IEnumerator HideCoinsDelta ()
    {
        yield return new WaitForSeconds (1f);
	//CoinsDeltaText.gameObject.SetActive (false);
    }

    private IEnumerator UpdateCoinsAmount ()
    {
    	// Animation for increasing and decreasing of coins amount
    	const float seconds = 0.5f;
    	float elapsedTime = 0;
    
    	while (elapsedTime < seconds) {
    	   // CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp (PreviousCoinsAmount, CurrentCoinsAmount, (elapsedTime / seconds))).ToString ();
    	    elapsedTime += Time.deltaTime;
    
    	    yield return new WaitForEndOfFrame ();
        }
    
    	//PreviousCoinsAmount = CurrentCoinsAmount;
    	//CurrentCoinsText.text = CurrentCoinsAmount.ToString ();
    }
}
