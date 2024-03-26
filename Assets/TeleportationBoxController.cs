using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEditor.Experimental;
using UnityEngine;

public class TeleportationBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField][Range(6,15)] float size = 2;
    private LineRenderer lineRenderer;
 
    void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,-Vector2.right * size);
        lineRenderer.SetPosition(1,new Vector2(-size,size));
        lineRenderer.SetPosition(2,Vector2.one * size);
        lineRenderer.SetPosition(3,new Vector2(size,-size));
        lineRenderer.SetPosition(4,Vector2.one * -size);
        lineRenderer.SetPosition(5,-Vector2.right * size);
    }
    
}

