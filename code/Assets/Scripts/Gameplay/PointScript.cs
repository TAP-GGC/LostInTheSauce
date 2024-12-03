using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Manages points, high scores, and player progress during gameplay
public class PointScript : MonoBehaviour
{    // References and player stats
    public Transform playerPosition;    // Tracks the player's position
    public GameObject player;           // Player GameObject reference
    public int points = 0;              // Current points scored during gameplay
    public int goldCount = 0;           // Total gold collected
    public int iceCreamCount = 0;       // Total ice cream collected
    public int totalPoints = 0;         // Total calculated score (points + item bonuses)
    public int highScore;               // High score tracker
    public int maxY = -2;               // Maximum height reached by the player
    public int minY;                    // Minimum height threshold for game over
    Text score;                         // UI text component for displaying scores
    public bool isScore;                // Script responsible fot current score
    public bool isHighScore;            // Script responsible for the high score
    string gameOver;                    // Tracks the "Game Over" state
    bool once;                          // Ensures that game over is only executed once

    void Start() // Runs once at the start of the game
    { // Lines 25-33 reset all player stats to 0 during the start of a new game session
        once = true;
        PlayerPrefs.SetInt("IceCream", 0);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetString("PlayerDestroyed", "false");
        score = GetComponent<Text>();
        PlayerPrefs.DeleteKey("GetScore");
        PlayerPrefs.SetString("GameOver", "false");
        PlayerPrefs.SetInt("MinY", -5);
        iceCreamCount = 0;    // Settiing initial value of 0
        if (isScore)
        { // Initialize score-related variables for gameplay
            points = 0;
            iceCreamCount = 0;
            goldCount = 0;
            score.text = "Score: " + points;
        }
        if (isHighScore)
        {// Initialize high score display
            if (PlayerPrefs.HasKey("Rank1Name"))
            {
                points = PlayerPrefs.GetInt("Rank1Score");
            }
            score.text = "High Score: " + points;
            points = 0;     // Reset points after displaying high score
        }
    }

    void Update()   // Runs once per frame
    {// Instantiating other scripts
        IceCreamScript ics = new IceCreamScript();
        HighScoreScript hss = new HighScoreScript();
        minY = maxY - 3;
        print(PlayerPrefs.GetInt("Gold"));
        gameOver = PlayerPrefs.GetString("GameOver");
        iceCreamCount = PlayerPrefs.GetInt("IceCream");
        goldCount = PlayerPrefs.GetInt("Gold");

        // Calculate total score based on collected items
        int iceCreamPoints = iceCreamCount * 350 + goldCount * 3000;
        totalPoints = points + iceCreamPoints;
        if (isScore)  // Update score display
        {
            score.text = "Score: " + totalPoints;
        }
        // Update high score display if applicable
        if(isHighScore)
        {
            if(totalPoints > PlayerPrefs.GetInt("Rank1Score"))
            {
                score.text = "High Score: " + totalPoints;
            }
        }
        // Update scores when the player surpasses maxY
        if ((int)playerPosition.position.y > maxY)
        {
            PlayerPrefs.SetInt("MinY", minY);
            if (isScore)
            {
                //print(PlayerPrefs.GetInt("IceCream"));
                //print(PlayerPrefs.GetInt("Gold"));
                maxY = (int)playerPosition.position.y;
                points += 1; // Increment points for height progress
                totalPoints = points + iceCreamPoints;
                score.text = "Score: " + totalPoints;
            }
            if (isHighScore)
            {
                maxY = (int)playerPosition.position.y;
                points += 1;
                totalPoints = points + iceCreamPoints;
                if (points > PlayerPrefs.GetInt("Rank1Score"))
                {
                    highScore = points;
                    score.text = "High Score: " + totalPoints;
                }
                else if (PlayerPrefs.GetInt("Rank1Score") > points)
                {
                    score.text = "High Score: " + PlayerPrefs.GetInt("Rank1Score");
                }
            }
        }
        // Trigger Game Over when player falls below minY
        if ((int)playerPosition.position.y <= minY && gameOver.Equals("false") && once)
        {
            PlayerPrefs.SetString("GameOver", "true");
            PlayerPrefs.SetInt("GetScore", totalPoints);
            once = false; // Prevent duplicate Game Over execution
            hss.NewHighScore(); // Save the new high score if applicable
            if (isHighScore)
            {
                points = 0;
            }
        }
         // Track if the player has been destroyed (currently unused)
        string destroyed = PlayerPrefs.GetString("PlayerDestroyed");
    }
}
