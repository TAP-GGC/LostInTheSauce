using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Manages the game's high score system, including ranking and UI updates
public class HighScoreScript : MonoBehaviour
{
    //ScoreObjects have (string name, int score)
    ArrayList highScoreArray = new ArrayList();    // Creating an arraylist to keep track of all high scores
    public List<ScoreObject> highScoreList = new List<ScoreObject>();    // List to use high score objects
    public int rank; // Initializes rank variable used to rank high scores
    Text scoreString;
    string playerName; // Initializes player name
    int score; // Variable for score

    void Start()
    {
        scoreString = GetComponent<Text>();
        List<ScoreObject> highScoreList = new List<ScoreObject>();
        if (!PlayerPrefs.HasKey("Rank1Name"))
        {
            for (int index = 0; index < 10; index++)
            {
                ScoreObject callScore = new ScoreObject();
                highScoreList.Add(callScore);
                SetScore(callScore.getName(), callScore.getScore());
            }
        }
        SetText();
    }

    void Update()
    {
        SetText();
    }

    public void NewHighScore()
    {
        score = PlayerPrefs.GetInt("GetScore");//"GetScore" comes from PointScript
        PlayerPrefs.SetString("LoopOnce", "true");
        score = PlayerPrefs.GetInt("GetScore");
        //playerName = PlayerPrefs.GetString("GetName");//
        int lowestScore = PlayerPrefs.GetInt("Rank10Score");
        print(PlayerPrefs.GetInt("Rank10Score"));
        if (score > lowestScore)
        {
            for (int index = 1; index <= 10; index++)
            {
                score = PlayerPrefs.GetInt("GetScore");
                int comparedScore = PlayerPrefs.GetInt("Rank" + index + "Score");
                PlayerPrefs.SetString("Rank" + index + "Name", "NUL");
                //string once = PlayerPrefs.GetString("LoopOnce");
                if (score > comparedScore && PlayerPrefs.GetString("LoopOnce").Equals("true"))
                {
                    //print("Type your name");
                    PlayerPrefs.SetString("GetName", Input.inputString);
                    PlayerPrefs.SetInt("TempScore", PlayerPrefs.GetInt("Rank" + index + "Score"));
                    PlayerPrefs.SetString("TempName", PlayerPrefs.GetString("Rank" + index + "Name"));
                    PlayerPrefs.SetInt("Rank" + index + "Score", PlayerPrefs.GetInt("GetScore"));
                    PlayerPrefs.SetString("Rank" + index + "Name", PlayerPrefs.GetString("GetName"));
                    PlayerPrefs.SetInt("GetScore", PlayerPrefs.GetInt("TempScore"));
                    PlayerPrefs.SetString("GetName", PlayerPrefs.GetString("TempName"));
                    PlayerPrefs.SetString("LoopOnce", "false");
                }
                else if (score > comparedScore)
                {
                    PlayerPrefs.SetInt("TempScore", PlayerPrefs.GetInt("Rank" + index + "Score"));
                    PlayerPrefs.SetString("TempName", PlayerPrefs.GetString("Rank" + index + "Name"));
                    PlayerPrefs.SetInt("Rank" + index + "Score", PlayerPrefs.GetInt("GetScore"));
                    PlayerPrefs.SetString("Rank" + index + "Name", PlayerPrefs.GetString("GetName"));
                    PlayerPrefs.SetInt("GetScore", PlayerPrefs.GetInt("TempScore"));
                    PlayerPrefs.SetString("GetName", PlayerPrefs.GetString("TempName"));
                }
            }
        }
        SetText();
    }
    public void SetScore(string playerName, int score)
    {
        PlayerPrefs.SetString("Rank" + rank + "Name", playerName);
        PlayerPrefs.SetInt("Rank" + rank + "Score", score);
    }

    public void SetText()
    {
        playerName = PlayerPrefs.GetString("Rank" + rank + "Name");
        score = PlayerPrefs.GetInt("Rank" + rank + "Score");
        //scoreString.text = "Rank" + rank + ": Score: " + score + " " + playerName;
        scoreString.text = "Rank" + rank + ": Score: " + score + " ";
    }
}
