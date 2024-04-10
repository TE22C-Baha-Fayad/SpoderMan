using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStarUnlock : MonoBehaviour
{

    //the starsui childs under this gameobject,they are changing colour if the user collects a star.
    List<GameObject> stars = new List<GameObject>(); 
    //the ammount of stars collected
    int starsCollected = 0;
    void Start()
    {
        //subscribe to the event to count stars
        CollectableItem.OnStarCollected += StarCollected;
        //gets the children under this gameobject to store them in the stars list
        for (int i = 0; i < transform.childCount; i++)
        {
            stars.Add(transform.GetChild(i).gameObject);
        }
    }

    //invoked when a star is collected
    void StarCollected()
    {
        //change the opacity of the star to full white.
        stars[starsCollected].GetComponent<RawImage>().color = Color.white;
        //star count increase.
        starsCollected++;
    }


}
