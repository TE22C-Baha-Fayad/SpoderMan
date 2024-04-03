using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    // TODO: show the player some instructions and make it visible that you can cancel a teleportation
    // TODO: skriv i loggboken**********
    // TODO: make pause menu
    // TODO: done (x) player jumping high by teleportation, fix that. 
    // TODO: start scene, levels, 
    // TODO: done (x) teleportation limit. 
    // TODO: lasers, obsticals such as sticks rotating in the way of the player.
    // TODO:  comment code later
    // TODO: done (x) wining after loosing glitch that must be fixed****
    // TODO: done (x) teleportationsAvailable count is wierd, fix that.
    // TODO: divide into more functions 
    [Header("Player Settings")]
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float teleportationLineSpeed = 3;
    [SerializeField] int teleportationsAvailable = 3;

    [Header("Collision Box Settings")]
    [SerializeField] Vector2 groundCollisionBoxSize = Vector2.one;
    [SerializeField] float castDistance = 1f;
    [SerializeField] LayerMask groundLayer;



    public delegate void Teleported(int teleportationsAvailable);
    public static event Teleported OnTeleport;


    private LineRenderer playerTeleportationLine;
    private GameObject teleportationBorder;
    private bool teleportActive = false;
    private bool isJumping = false;
    private Rigidbody2D rb;


    void Start()
    {
        CanvasController.OnGameEnded += GameEnded;
        teleportationBorder = transform.Find("TeleportationBorder").gameObject;
        playerTeleportationLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        OnTeleport?.Invoke(teleportationsAvailable);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && teleportActive)
        {
            CancelTeleportation();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !teleportActive)
        {
            teleportActive = true;
            playerTeleportationLine.SetPosition(1, Vector3.zero);
            if (teleportationsAvailable > -1)
                teleportationsAvailable--;

            OnTeleport?.Invoke(teleportationsAvailable);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && teleportActive)
        {
            teleportActive = false;
            transform.position = transform.TransformPoint(playerTeleportationLine.GetComponent<LineRenderer>().GetPosition(1));
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() && !teleportActive)
        {
            isJumping = true;
        }






    }
    void FixedUpdate()
    {
        if (teleportActive && teleportationsAvailable > -1)
        {
            teleportationBorder.SetActive(true);
            playerTeleportationLine.enabled = true;
            Teleport();


        }
        else
        {
            teleportationBorder.SetActive(false);
            playerTeleportationLine.enabled = false;
            if (isJumping)
            {
                HandleJump();
                isJumping = false;
            }
            HandleMovement();


        }
    }

    void GameEnded()
    {
        gameObject.SetActive(false);
    }
    /* void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBoxSize);
    } */
    bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCollisionBoxSize, 0, -transform.up, castDistance, groundLayer))
            return true;
        else
            return false;
    }
    void CancelTeleportation()
    {
        teleportationsAvailable++;
        OnTeleport?.Invoke(teleportationsAvailable);
        teleportActive = false;
    }
    void Teleport()
    {
        Vector3 lineMovement = new Vector3(playerTeleportationLine.GetPosition(1).x, playerTeleportationLine.GetPosition(1).y, 0);
        LineRenderer borderLine = teleportationBorder.GetComponent<LineRenderer>();

        if (Input.GetKey(KeyCode.W))
        {
            if (playerTeleportationLine.GetPosition(1).y < borderLine.GetPosition(1).y - borderLine.widthMultiplier)
                lineMovement.y += teleportationLineSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (playerTeleportationLine.GetPosition(1).x < borderLine.GetPosition(2).x - borderLine.widthMultiplier)
                lineMovement.x += teleportationLineSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (playerTeleportationLine.GetPosition(1).y > borderLine.GetPosition(4).y + borderLine.widthMultiplier)
                lineMovement.y -= teleportationLineSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (playerTeleportationLine.GetPosition(1).x > borderLine.GetPosition(4).x + borderLine.widthMultiplier)
                lineMovement.x -= teleportationLineSpeed * Time.deltaTime;
        }


        playerTeleportationLine.SetPosition(1, lineMovement);

    }
    void HandleMovement()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed;

        if (movement.x > 0)
        {

            Animation(true);
        }
        else if (movement.x < 0)
        {

            Animation(false);
        }

        transform.Translate(movement, Space.World);


        void Animation(bool directionRight)
        {


            if (directionRight)
            {
                sprite.flipX = true;
            }
            else if (!directionRight)
            {

                sprite.flipX = false;
            }

        }

    }
    void HandleJump()
    {

        rb.AddForce(new Vector2(0, jumpForce));

    }


}
