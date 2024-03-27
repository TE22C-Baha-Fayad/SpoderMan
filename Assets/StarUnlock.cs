using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUnlock : MonoBehaviour
{
    List<GameObject> stars = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i<transform.childCount; i++)
        {
            stars.Add(transform.GetChild(i).gameObject);
        }
    }

    
}
