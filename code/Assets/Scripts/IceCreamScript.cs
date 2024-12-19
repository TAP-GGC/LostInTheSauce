using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
// Handles interactions with ice cream and coin tiles in the game
public class IceCreamScript : MonoBehaviour
{
// Flags to determine if the object is a regular or gold ice cream
    public bool isRegular;
    public bool isGold;

    // Reference to the tile representing a coin or ice cream
    public Tile pointTile;
   // public GameObject tilemapPointTile;
    public int iceCreamCount;
    public int goldCount;
   
    // References to the Tilemap and GameObject for coins
    public GameObject tilemapCoin;
    public Tilemap tilemap;

    // Reference to the Generator script (possibly for procedural generation)
    public Generator gen;
    
    // Tilemap used for coin management (unused in this version)
    Tilemap coinTM;
    // Start is called before the first frame update
    void Start()
    {
        // Find the Generator script in the scene
        gen = FindObjectOfType<Generator>();

        // Initialize PlayerPrefs for ice cream and gold counts
        PlayerPrefs.SetInt("IceCream", 0);
        iceCreamCount = 0;

        // Initialize regular ice cream if the object is marked as regular
        if (isRegular)
        {
            PlayerPrefs.SetInt("IceCream", 0);
            iceCreamCount = 0;
        }
        
        // Initialize gold ice cream if the object is marked as gold
        if (isGold)
        {
            PlayerPrefs.SetInt("Gold", 0);
            goldCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Handles 2D collision detection with the player
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.LogWarning(col.gameObject.tag);
        if (col.gameObject.tag == "coin")
        {
            // Log the tag of the collided object (for debugging purposes)
            Debug.LogWarning("coin should Snatch");
            
            // Initialize a position vector for the collision point
            Vector3Int hitPos = Vector3Int.zero;
            Debug.LogWarning(hitPos);

            // Iterate through all collision contact points
            foreach (ContactPoint2D hit in col.contacts)
            {
                Debug.LogWarning(hit);

                // Convert the collision point to a tilemap coordinate
                hitPos.x = (int)hit.point.x;
                hitPos.y = (int)hit.point.y;
                Debug.LogWarning(hitPos);
                
                // Remove the corresponding tile from the tilemap
                tilemap.SetTile(hitPos, null);
                Debug.LogWarning(hitPos);

                // Reset the position vector for the next iteration
                hitPos = Vector3Int.zero;

                // Increment the ice cream count
                iceCreamCount++;

                // Update the ice cream count in PlayerPrefs
                PlayerPrefs.SetInt("IceCream", iceCreamCount);
            }
        }
    }
}
