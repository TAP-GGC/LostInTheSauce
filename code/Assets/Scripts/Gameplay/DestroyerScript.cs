using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the lifecycle of a prefab, destroying it if it moves below a certain vertical threshold.
public class DestroyerScript : MonoBehaviour
{
    public GameObject prefab;

    // Reference to the Transform of the prefab to track its position in the game world.
    public Transform prefabPosition;
    
    // Vertical boundaries for destroying the prefab.
    public int minY;  // The lower limit for destruction.
    public int maxY = -2; // The initial upper limit (default -2).

    // Update is called once per frame and handles prefab destruction logic.
    void Update()
    {
         // Update maxY if the prefab's current vertical position exceeds the existing maxY.
        if (prefabPosition.position.y > maxY)
        {
            maxY = (int) prefabPosition.position.y;
        }
            // Calculate minY as 3 units below maxY to set the destruction threshold.
            minY = maxY - 3;

        // If the prefab's vertical position falls below minY, destroy the prefab.
        if (prefabPosition.position.y < minY)
        {
            Destroy(prefab);
        }
    }
}
