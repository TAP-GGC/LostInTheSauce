using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIceCream : MonoBehaviour
{
    public GameObject iceCream;
    public GameObject gold;
    public Transform playerPos;
    public int itemYPos;
    public int maxY = -2;
    public int goldYSpawn = 8;
    public bool isWut;
    public bool isMainMenu;
    public bool isTimed;
    private float timeCount = 0.0f;
    private float timeThreshold = 0.0f;
    private int totalIceCream;

    void Start()
    {
        PlayerPrefs.SetInt("PrefabCount", 0);
    }
    private void createIceCream()
    {
        if (isWut)
        {
            itemYPos = (int)playerPos.position.y + 10;
            int randNum = Random.Range(-5, 4);
            GameObject item = Instantiate(iceCream, new Vector3Int(randNum, itemYPos, 0), Quaternion.identity) as GameObject;
            PlayerPrefs.SetInt("PrefabCount", PlayerPrefs.GetInt("PrefabCount") + 1);
        }
        if(isMainMenu)
        {
            int randNum = Random.Range(-150, 150);
            GameObject item = Instantiate(iceCream, new Vector3Int(randNum, 120, 91), Quaternion.identity) as GameObject;
        }
    }

    private void createGold()
    {
        itemYPos = (int)playerPos.position.y + 10;
        int randNum = Random.Range(-5, 4);
        GameObject item = Instantiate(gold, new Vector3Int(randNum, itemYPos, 0), Quaternion.identity) as GameObject;
    }

    void Update()
    {
        if (isWut)//If the camera is in the playing area, then perform these methods/do these
        {
            timeCount = Time.time;
            if (timeCount > timeThreshold)//Creates ice cream every 0.5 seconds
            {
               
                timeThreshold += 0.5f;//Change the timeThreshold to change the time Ice Cream spawns
            }
          
            if ((int)playerPos.position.y > maxY)//Only creates ice cream when player goes above their highest point
            {
                maxY = (int)playerPos.position.y;
                createIceCream();
            }
            if((int)playerPos.position.y > goldYSpawn)//Creates gold every 10 walls
            {
                goldYSpawn += 10;
                createGold();
            }
        }

        
        if (isTimed)//If the camera is in the playing area, then perform these methods/do these
        {
            if (PlayerPrefs.GetInt("PrefabCount") <= 4)//If there is less than or 4 Ice Cream, then make another Ice Cream
                //prefab. In other words, there can only be 5 Ice Cream on the screen at a time(ISV2)
            {
                timeCount = Time.time;
                if (timeCount > timeThreshold)//Creates ice cream every 0.5 seconds
                {
                    createIceCream();
                    timeThreshold += 0.5f;//Change the timeThreshold to change the time Ice Cream spawns
                }
                //createIceCream();//Keeps creating ice cream
                if ((int)playerPos.position.y > maxY)//Only creates ice cream when player goes above their highest point
                {
                    maxY = (int)playerPos.position.y;
                    createIceCream();
                }
            }
            if ((int)playerPos.position.y > goldYSpawn)//Creates gold every 10 walls
            {
                goldYSpawn += 10;
                createGold();
            }
        }
        
        

        if (isMainMenu)//If the camera is in the Main Menu, then perform these methods/do these
        {
            createIceCream();
        }
    }
}
