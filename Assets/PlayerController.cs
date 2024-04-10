using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // för mig bara
    // TODO: show the player some instructions and make it visible that you can cancel a teleportation done(x)
    // TODO: skriv i loggboken********** done(X)
    // TODO: make pause menu done(x)
    // TODO: done (x) player jumping high by teleportation, fix that. 
    // TODO: start scene, levels, done(x) partially
    // TODO: done (x) teleportation limit. 
    // TODO: lasers, obsticals such as sticks rotating in the way of the player. not enough time
    // TODO:  comment code later done(x)
    // TODO: done (x) wining after loosing glitch that must be fixed****
    // TODO: done (x) teleportationsAvailable count is wierd, fix that.
    // TODO: divide into more functions done(x)
    //för mig bara

    [Header("Player Settings")]
    [SerializeField][Tooltip("player movemenet speed")] float speed = 5;
    [SerializeField][Tooltip("player jumpforce")] float jumpForce = 1;
    [SerializeField][Tooltip("player teleportationbox line Speed")] float teleportationLineSpeed = 3;
    [SerializeField][Tooltip("player teleportations available")] int teleportationsAvailable = 3;

    [Header("Collision Box Settings")]
    [SerializeField][Tooltip("collision box for ground detection")] Vector2 groundCollisionBoxSize = Vector2.one;
    [SerializeField][Tooltip("collision box cast distance")] float castDistance = 1f;
    [SerializeField][Tooltip("collision layer")] LayerMask groundLayer;

    [Header("PauseGame Refrence")]
    [SerializeField][Tooltip("the canvas for the escape menu")] GameObject escapeMenu;


    //delegate for teleportation, used for Ui
    public delegate void Teleported(int teleportationsAvailable, bool teleportActive);
    public static event Teleported OnTeleport;


    //the line the player can control
    private LineRenderer playerTeleportationLine;
    //the teleportation box gameobject
    private GameObject teleportationBorder;
    //if the the player is teleporting
    private bool teleportActive = false;
    //if the player is jumping
    private bool isJumping = false;
    //if the game is paused
    private bool gamePaused = false;

    //player rb
    private Rigidbody2D rb;


    void Start()
    {
        Time.timeScale = 1; //make sure the game is not paused when the game begins
        SceneManager.activeSceneChanged += ResetValues;
        CanvasController.OnGameEnded += DisableGamobject;
        teleportationBorder = transform.Find("TeleportationBorder").gameObject;
        playerTeleportationLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        OnTeleport?.Invoke(teleportationsAvailable, teleportActive); //send the teleportations left count on start.
    }


    // Update is called once per frame
    void Update()
    {
        //if escape pressed and player isnt teleporting and the game isn't paused
        if (Input.GetKeyDown(KeyCode.Escape) && !teleportActive && !gamePaused)
        {
            //pause the game 
            gamePaused = true;
            GameStatePause(true);
        }//if the same applies but game is paused
        else if (Input.GetKeyDown(KeyCode.Escape) && !teleportActive && gamePaused)
        {
            //unpause the game
            gamePaused = false;
            GameStatePause(false);
        }
        //if escape is pressed and player is teleporting
        if (Input.GetKeyDown(KeyCode.Escape) && teleportActive)
        {
            CancelTeleportation();
        }
        //if space pressed and teleportation mode isn't activated.
        if (Input.GetKeyDown(KeyCode.Space) && !teleportActive)
        {
            //handeling the input for teleportation
            teleportActive = true; //activate the teleportation state
            playerTeleportationLine.SetPosition(1, Vector3.zero); //reset line position
            if (teleportationsAvailable > -1)
                teleportationsAvailable--; // subtract teleps left with 1 after a teleportation is made

            OnTeleport?.Invoke(teleportationsAvailable, teleportActive); //report the info to the textui.
        }
        else if (Input.GetKeyDown(KeyCode.Space) && teleportActive)
        {
            //handeling the input for teleportation
          
            transform.position = transform.TransformPoint(playerTeleportationLine.GetComponent<LineRenderer>().GetPosition(1)); //set the position of the player to the target
            teleportActive = false;//stop the teleportation 
            OnTeleport?.Invoke(teleportationsAvailable,teleportActive);
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

    void DisableGamobject()
    {
        gameObject.SetActive(false);
    }
    /* void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBoxSize);
    } */

    /// <summary>
    /// checks for ground for player
    /// </summary>
    /// <returns>true if the player is grounded</returns>
    bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCollisionBoxSize, 0, -transform.up, castDistance, groundLayer))
            return true;
        else
            return false;
    }
    /// <summary>
    /// cancels the teleportation
    /// </summary>
    void CancelTeleportation()
    {
        teleportationsAvailable++; //increasing the teleportation that would have been lost.
        teleportActive = false; // setting teleportation state to false.
        OnTeleport?.Invoke(teleportationsAvailable, teleportActive); //reporting the count of teleportation to ui
    }
    void Teleport()
    {
        //line movment vector
        Vector3 lineMovement = new Vector3(playerTeleportationLine.GetPosition(1).x, playerTeleportationLine.GetPosition(1).y, 0);
        //borderline component
        LineRenderer borderLine = teleportationBorder.GetComponent<LineRenderer>();

        // by input move the line in the wanted direction only if the line didn't reach the border.
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
    /// <summary>
    /// handels the player movement
    /// </summary>
    void HandleMovement()
    {
        //gets the sprite for the player
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //vector2 for movement
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed;

        //if the player moves right 
        if (movement.x > 0)
        {
            //make the sprite look right by flipping the x axis
            sprite.flipX = true;
        }
        //if the player moves left 
        else if (movement.x < 0)
        {
            //make the sprite look left by flipping the x axis
            sprite.flipX = false;
        }
        //the actual movement
        transform.Translate(movement);
    }
    /// <summary>
    /// handels the jumping for the player
    /// </summary>
    void HandleJump()
    {
        // adds force on the y axis
        rb.AddForce(new Vector2(0, jumpForce));
    }

    /// <summary>
    /// turns the game state either to paused or un paused by setting the timescale to 0 or 1 and switching the escape menu state.
    /// </summary>
    /// <param name="paused"></param>
    void GameStatePause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            escapeMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            escapeMenu.SetActive(false);
        }

    }
    void ResetValues(Scene current, Scene next)
    {
        OnTeleport = null;
    }

}
