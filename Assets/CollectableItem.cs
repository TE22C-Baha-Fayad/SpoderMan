using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    public delegate void StarCollected();

    public static event StarCollected onStarCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onStarCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
