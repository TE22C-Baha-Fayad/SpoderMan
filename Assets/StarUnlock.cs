using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUnlock : MonoBehaviour
{

    List<GameObject> stars = new List<GameObject>();
    int starsCollected = 0;
    void Start()
    {
        CollectableItem.OnStarCollected += StarCollected;
        for (int i = 0; i < transform.childCount; i++)
        {
            stars.Add(transform.GetChild(i).gameObject);
        }

    }

    void StarCollected()
    {
        stars[starsCollected].GetComponent<RawImage>().color = Color.white;
        starsCollected++;
    }


}
