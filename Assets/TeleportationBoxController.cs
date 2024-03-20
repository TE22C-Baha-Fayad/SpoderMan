using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEditor.Experimental;
using UnityEngine;

public class TeleportationBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField][Range(1,3)] float size = 2;
    
    void Awake()
    {
        transform.localScale = new Vector3(size,size,1);
    }
    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = new Vector3(size,size,1);
        
    }
}
