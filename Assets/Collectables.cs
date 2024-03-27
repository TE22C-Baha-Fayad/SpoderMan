using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    // Start is called before the first frame update
    public static int starsCount;
    void Awake()
    {
        starsCount = transform.Find("Stars").transform.childCount;
        
    }

}
