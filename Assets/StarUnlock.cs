using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUnlock : MonoBehaviour
{

    List<GameObject> stars = new List<GameObject>();
    void Start()
    {
        CollectableItem.onStarCollected += OnStarCollected;
        for (int i = 0; i < transform.childCount; i++)
        {
            stars.Add(transform.GetChild(i).gameObject);
        }

    }
    int starsCollected = 0;
    public void OnStarCollected()
    {
        stars[starsCollected].GetComponent<RawImage>().color = Color.white;
        starsCollected++;

    }


}
