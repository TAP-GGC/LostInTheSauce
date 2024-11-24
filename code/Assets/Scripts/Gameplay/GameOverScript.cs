using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the game-over functionality by monitoring a target prefab (e.g., the player)
// and toggling the visibility of a game-over screen when necessary.
public class GameOverScript : MonoBehaviour
{
    // Reference to the target object (e.g., player or another critical object to monitor).
    public GameObject prefab;

     // Reference to the Game Over UI screen.
    public GameObject gameOverScreen;

    // Static variable to track whether the player is "dead."
    // Shared across the application to allow other scripts to update this state.
    public static bool dead = false;

    // Update is called once per frame to check game-over conditions.
    void Update()
    {
        // If the player is marked as "dead," hide the game-over screen.
        // This might be part of resetting or clearing the game-over state.
        if (dead == true)
        {
            gameOverScreen.SetActive(false);
        }
        // If the monitored prefab (e.g., player) no longer exists (is null), display the game-over screen.
        if (prefab == null)
        {
            gameOverScreen.SetActive(true);
            //PlayerPrefs.SetString("GameOver", "true");
        }
    }
}
