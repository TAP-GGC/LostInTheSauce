using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles sound effects for various game actions, like jumping and game events
public class SoundEffects : MonoBehaviour
{
    // Sound effect clips for different actions
    public AudioClip jumpingSound, damageSound, deathSound, gameOverSound;
    public AudioClip soundClip; // General-purpose sound clip
    public AudioClip jump; // Separate variable for jumping sound (redundant with jumpingSound)

    // Audio source component responsible for playing the sounds
    public AudioSource audioSound;
    
    void Start()
    {
        AudioSource audioSound = GetComponent<AudioSource>();    // Get the AudioSource component attached to the GameObject
        jumpingSound = Resources.Load<AudioClip>("JumpSFX");    // Load the jumping sound from the Resources folder
        jump = Resources.Load<AudioClip>("JumpSFX");           // Load another instance of the jumping sound (redundant, same as jumpingSound)
        audioSound = GetComponent<AudioSource>();             // Assign the AudioSource component again (redundant, already assigned above)
    }

    void Update()
    {
        // Check for the space bar press to play the jump sound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            {
                audioSound.PlayOneShot(jumpingSound);  // Play the jump sound
            }
        }
    }
    
    // Plays a specific sound effect based on a string identifier
    // Parameters:
    //   sound: The name of the sound to play
    public void soundEffect(string sound)
    {
        // Create a new instance of the SoundEffects class (not needed, redundant)
        SoundEffects se = new SoundEffects();
        // Play the jump sound if the string matches "JumpSFX"
        if (sound.Equals("JumpSFX"))
        {
            se.audioSound.PlayOneShot(jumpingSound);
        }
        // Handle specific sound cases using a switch statement
        switch (sound)
        {
            case "JumpSFX":
                se.soundClip = se.jumpingSound;               // Assign the jump sound to the general-purpose clip
                se.audioSound.PlayOneShot(jumpingSound);     // Play the jump sound
                print("Sound!");                            // Debug message
                break;
                
        }
    }
}
