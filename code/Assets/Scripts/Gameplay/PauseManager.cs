using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages the pause state of the game, including UI and time scale adjustments
public class PauseManager : MonoBehaviour
{
    public static bool paused = false;     // Static variable to track whether the game is currently paused
    public GameObject pauser;    // Reference to the pause menu UI GameObject
    public bool isMainMenu;     // Flag to indicate if this script is used in the main menu. If true, then the pause function is ignored completely.

    void Start()     // Called when the script instance is loaded
    {
        Resume();    // Ensure the game starts in a resumed state
    }

    void Update()     // Called once per frame
    {
        if(isMainMenu)  // If the scene is the main menu, ensure the game is not paused
        {
            Resume();
        }
        if (Input.GetKeyDown(KeyCode.Escape))    // Toggle pause state when the Escape key is pressed
        {
            if (paused)
            {
                Resume(); // Resume the game if currently paused
            }
            else
            {
                Pause(); // Pause the game if currently running
            }
        }
    }

    public void Resume()     // Resumes the game by hiding the pause menu and setting time scale to normal
    {
        if (isMainMenu)
        {
            Time.timeScale = 1f;  // In the main menu, just ensure the game is running
            paused = false;
        }
        if (!isMainMenu)
        {
            pauser.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
    }

    void Pause()     // Pauses the game by showing the pause menu and freezing time
    {
        pauser.SetActive(true);         // Show the pause menu
        Time.timeScale = 0f;         // Stop time in the game
        paused = true;         // Set paused state to true
    }
}
