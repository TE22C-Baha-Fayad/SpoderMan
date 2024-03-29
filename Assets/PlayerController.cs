using UnityEditor.Search;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update¨
    // TODO: start scene, levels, 
    // TODO: teleportation limit
    // TODO: lasers, obsticals such as sticks rotating in the way of the player.
    // TODO:  comments
    // TODO: be able to cancell teleportation without cost
    // TODO: wining after loosing glitch that must be fixed****
    // TODO: teleportationsAvailable count is wierd, fix that.
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float teleportationBorderSpeed = 3;

    public delegate void Teleported(int tel);
    public static event Teleported onTeleport;
    [SerializeField] int teleportationsAvailable = 3;
    [SerializeField] Vector2 groundCheckBoxSize = Vector2.one;
    [SerializeField] float castDistance = 1f;
    [SerializeField] LayerMask groundLayer;


    private LineRenderer playerTeleportationLine;
    private GameObject teleportationBorder;
    private bool teleportActive = false;
    private bool isJumping = false;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        teleportationBorder = transform.Find("TeleportationBorder").gameObject;
        playerTeleportationLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onTeleport?.Invoke(teleportationsAvailable);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && teleportActive)
        {
            cancellTeleportation();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !teleportActive)
        {
            teleportActive = true;
            playerTeleportationLine.SetPosition(1, Vector3.zero);
            if (teleportationsAvailable > -1)
                teleportationsAvailable--;

            onTeleport?.Invoke(teleportationsAvailable);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && teleportActive)
        {
            teleportActive = false;
            transform.position = transform.TransformPoint(playerTeleportationLine.GetComponent<LineRenderer>().GetPosition(1));

        }


        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            isJumping = true;
        }



    }
    void FixedUpdate()
    {
        if (teleportActive && teleportationsAvailable > -1)
        {
            teleportationBorder.gameObject.SetActive(true);
            playerTeleportationLine.enabled = true;
            Teleport();


        }
        else
        {
            teleportationBorder.gameObject.SetActive(false);
            playerTeleportationLine.enabled = false;
            Movement();
            if (isJumping)
            {
                Jump();
                isJumping = false;
            }


        }
    }
    /* void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBoxSize);
    } */
    bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.up, castDistance, groundLayer))
            return true;
        else
            return false;

    }

    void cancellTeleportation()
    {

        teleportationsAvailable++;
        onTeleport?.Invoke(teleportationsAvailable);
        teleportActive = false;


    }
    void Teleport()
    {
        Vector3 lineMovement = new Vector3(playerTeleportationLine.GetPosition(1).x, playerTeleportationLine.GetPosition(1).y, 0);
        LineRenderer borderLine = teleportationBorder.GetComponent<LineRenderer>();

        if (Input.GetKey(KeyCode.W))
        {
            if (playerTeleportationLine.GetPosition(1).y < borderLine.GetPosition(1).y - borderLine.widthMultiplier)
                lineMovement.y += teleportationBorderSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (playerTeleportationLine.GetPosition(1).x < borderLine.GetPosition(2).x - borderLine.widthMultiplier)
                lineMovement.x += teleportationBorderSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (playerTeleportationLine.GetPosition(1).y > borderLine.GetPosition(4).y + borderLine.widthMultiplier)
                lineMovement.y -= teleportationBorderSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (playerTeleportationLine.GetPosition(1).x > borderLine.GetPosition(4).x + borderLine.widthMultiplier)
                lineMovement.x -= teleportationBorderSpeed * Time.deltaTime;
        }


        playerTeleportationLine.SetPosition(1, lineMovement);

    }
    void Movement()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed;


        string lookingRightString = "LookingRight";
        if (movement.x > 0)
        {
            animator.SetBool(lookingRightString, false);

        }
        else if (movement.x < 0)
        {

            animator.SetBool(lookingRightString, true);
        }
        transform.Translate(movement, Space.World);
    }
    void Jump()
    {

        rb.AddForce(new Vector2(0, jumpForce));

    }


}
