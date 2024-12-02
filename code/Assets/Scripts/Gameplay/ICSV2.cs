using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICSV2 : MonoBehaviour
{
    // Tracks the count of ice cream items collected by the player
    public int iceCreamCount;
    
     // Tracks the count of gold items collected by the player
    public int goldCount;
    
    // Determines if the object is in the main menu (affects destruction logic)
    public bool isMainMenu;
    
    // Determines if this object represents a gold item
    public bool isGold;

     // Called once per frame
    void Update()
    {

        // Check if the object should be destroyed based on its position
        // Different logic for main menu and gameplay scenes
        if (!isMainMenu)
        {
            // Retrieve the minimum Y value from PlayerPrefs
            int minY = PlayerPrefs.GetInt("MinY");

            // Destroy the object if it falls below the minimum Y threshold
            if ((int)gameObject.transform.position.y < minY)
            {
                Destroy(this.gameObject);
            }
        }
        if(isMainMenu)
        {
            int minY = -100; // Fixed minimum Y threshold for the main menu

            // Destroy the object if it falls below the threshold
            if((int)gameObject.transform.position.y < minY)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Called when the object collides with another 2D collider
    void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the colliding object is tagged as "Player"
        if (col.gameObject.tag == "Player" && !isGold)
        {
            // Handle ice cream collection
            // Retrieve the current ice cream count from PlayerPrefs
            iceCreamCount = PlayerPrefs.GetInt("IceCream");
            
            // Increment the ice cream count
            iceCreamCount += 1;

            // Update the ice cream count in PlayerPrefs
            PlayerPrefs.SetInt("IceCream", iceCreamCount);

            // Decrement the prefab count in PlayerPrefs
            PlayerPrefs.SetInt("PrefabCount", PlayerPrefs.GetInt("PrefabCount") - 1);
            Destroy(this.gameObject);
        }
        else if(isGold) // Handle gold collection
        {
            goldCount = PlayerPrefs.GetInt("Gold"); // Retrieve the current gold count from PlayerPrefs
            goldCount += 1;
            PlayerPrefs.SetInt("Gold", goldCount);
            Destroy(this.gameObject);
        }
    }
}
