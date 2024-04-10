using UnityEngine;

public class TeleportationBoxController : MonoBehaviour
{
    [SerializeField][Range(6,20)][Tooltip("changes the size of the box")]float size = 2; // the size of the teleportation box
    //get's the line renderer for the game
    private LineRenderer lineRenderer;
    // on validate is called in the editor and game run if changes are made to the variables that's serialized.
    void OnValidate()
    {
        // sets the boundaries for the linerenderer to form a box
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,-Vector2.right * size);
        lineRenderer.SetPosition(1,new Vector2(-size,size));
        lineRenderer.SetPosition(2,Vector2.one * size);
        lineRenderer.SetPosition(3,new Vector2(size,-size));
        lineRenderer.SetPosition(4,Vector2.one * -size);
        lineRenderer.SetPosition(5,-Vector2.right * size);
    }
    
}

