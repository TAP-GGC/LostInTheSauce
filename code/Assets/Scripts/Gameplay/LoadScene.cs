using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for managing scene loading


public class LoadScene : MonoBehaviour
{
    // Loads a scene by name, with validation to prevent runtime errors
    public void loadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Exit() // Method to quit the application
    {
        Application.Quit();
    }
}
