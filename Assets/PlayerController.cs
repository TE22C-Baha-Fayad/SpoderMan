using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update¨

    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 1;

    [SerializeField] float teleportationLineSpeed = 3;

    private LineRenderer teleportationLine;
    private bool teleportActive = false;
    private Animator animator;
    private Rigidbody2D rb;



    void Start()
    {
        teleportationLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !teleportActive)

            teleportActive = true;
        else if(Input.GetKeyDown(KeyCode.Space) && teleportActive)
        {
            teleportActive = false;
        }
        

        if(teleportActive)
        {
            transform.Find("TeleportationBorder").gameObject.SetActive(true);
            teleportationLine.enabled = true;
            Teleport();
        }
        else{
            transform.Find("TeleportationBorder").gameObject.SetActive(false);
            teleportationLine.enabled = false;
            Movement();
        }
        

    }

    void Teleport()
    {
        Vector3 lineMovement = new Vector3(teleportationLine.GetPosition(1).x, teleportationLine.GetPosition(1).y, 0);
        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            lineMovement.y += teleportationLineSpeed *Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            lineMovement.x += teleportationLineSpeed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            lineMovement.y -= teleportationLineSpeed*Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            lineMovement.x -= teleportationLineSpeed*Time.deltaTime;
        }
        
        
        teleportationLine.SetPosition(1, lineMovement);
    }
    void Movement()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed;


        string LookingRight = "LookingRight";
        if (movement.x > 0)
        {
            animator.SetBool(LookingRight, false);

        }
        else if (movement.x < 0)
        {

            animator.SetBool(LookingRight, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        transform.Translate(movement, Space.World);
    }
}
