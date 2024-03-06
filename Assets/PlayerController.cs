using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update¨

    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce =1;

    private Animator animator;
    private Rigidbody2D rb;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"),0) *Time.deltaTime *speed;
        
        
        string LookingRight = "LookingRight";
        if(movement.x > 0)
        {
            animator.SetBool(LookingRight,true);
            
        }
        else if(movement.x < 0)
        {

            animator.SetBool(LookingRight,false);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0,jumpForce));
        }
      
        transform.Translate(movement,Space.World);
    }
}
