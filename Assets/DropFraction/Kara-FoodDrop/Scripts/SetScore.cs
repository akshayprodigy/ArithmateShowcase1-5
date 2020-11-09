using UnityEngine;
using System.Collections;

public class SetScore : MonoBehaviour 
{
	public TextMesh bestScoreLabel;
	public TextMesh scoreLabel;

	void Start () 
	{
		scoreLabel.text = "Score: " + GameManager1.score.ToString();

		if (GameManager1.score > 0)
		{
			if (PlayerPrefs.GetInt("Score", 0) < GameManager1.score)
			{
				PlayerPrefs.SetInt("Score", GameManager1.score);
				PlayerPrefs.Save();
			}
		}

		bestScoreLabel.text = "HighScore: " + PlayerPrefs.GetInt("Score", 0).ToString();
		GameManager1.score = 0;
	}
}