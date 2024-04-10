using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollectableItem : MonoBehaviour
{
    //delegate for starscollection
    public delegate void StarCollected();

    public static event StarCollected OnStarCollected;


    void OnTriggerEnter2D(Collider2D collision)
    {
        //on scene changes reset the static events values
        SceneManager.activeSceneChanged += ResetValues;
        // compares if tags are equal between the player 
        if (CompareTag(collision.gameObject.tag) == CompareTag("Player"))
        {   
            //invoke star collected
            OnStarCollected?.Invoke();
            //destroy the star gameobject
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// reset static values such as for static events.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="next"></param>
    void ResetValues(Scene current, Scene next)
    {
        OnStarCollected = null;
    }



}
