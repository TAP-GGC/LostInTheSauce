//Importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class decloration, defined camerafollow class and inherits from Unity's MonoBehaviour base class. CameraFollow is now a Unity script that can be attached to a gameobject (our player)
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float maxY = -2.0f;

    void FixedUpdate()
    {
        //transform.position = new Vector3(0, player.position.y, transform.position.z);//Normal Camera Movement
        transform.position = new Vector3(0, maxY + 3.5f, transform.position.z);//Vertical Camera Movement
        if (player.position.y > maxY)
        {
            maxY = player.position.y;
        }
    }


}
