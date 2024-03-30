using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollectableItem : MonoBehaviour
{
    public delegate void StarCollected();

    public static event StarCollected OnStarCollected;

    //works but stars don't get highlighted
    void Start()
    {
        //onStarCollected = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.activeSceneChanged += ResetValues;
        if (collision.gameObject.tag == "Player")
        {
            OnStarCollected?.Invoke();
            Destroy(gameObject);
        }
    }
    void ResetValues(Scene current, Scene next)
    {
        OnStarCollected = null;
    }



}
