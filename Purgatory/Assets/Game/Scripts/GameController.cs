using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public float lengthOfGameInMinutes;
	public Text angelScoreText;
	public Text demonScoreText;


    private static uint scoreAngel;
    private static uint scoreDemon;
    private float timeRemaining;

	// Use this for initialization
	void Start () {

        timeRemaining = 60.0f * lengthOfGameInMinutes;
	//	scoreAngel = 0;
	//	scoreDemon = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
            GameOver();

		angelScoreText.text = "" + scoreAngel;
		demonScoreText.text = "" + scoreDemon;
	}

    public void gainScoreAngel(uint value)
    {
        scoreAngel += value;
    }

    public void gainScoreDemon(uint value)
    {
        scoreDemon += value;
    }

    public void GameOver()
    {
        if (scoreAngel > scoreDemon)
        {
            print("L'ange remporte la partie !");
        }
        else if (scoreDemon > scoreAngel)
        {
            print("Le démon remporte la partie !");
        }
        else
        {
            print("Le démon et l'ange sont ex aequo !");
        }
    }

    //GETTERS
    public uint getScoreAngel() { return scoreAngel; }
    public uint getScoreDemon() { return scoreDemon; }

    //SETTERS
    public void setScoreAngel(uint value) { scoreAngel = value; }
    public void setScoreDemon(uint value) { scoreDemon = value; }
}
