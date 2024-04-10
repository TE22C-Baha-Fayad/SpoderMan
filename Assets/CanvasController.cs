using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [Header("Win Stars Managment")]
    [SerializeField][Tooltip("the starsUi in the win canvas.")] GameObject starsUI;
    [SerializeField][Tooltip("the textures to swap for stars depending on how many the player earn.")] Texture[] starTextures;
    [SerializeField][Tooltip("the actual stars gameObject container to get the child stars count.")] GameObject stars;

    [Header("Canvases")]
    [SerializeField][Tooltip("wincanvas gameobject")] GameObject winCanvas;
    [SerializeField][Tooltip("Losecanvas gameobject")] GameObject loseCanvas;

    [Header("Other")]
    [SerializeField][Tooltip("the player gameobject")] GameObject player;

    //delegate for when gameEnds
    public delegate void GameEnded();
    public static event GameEnded OnGameEnded;

    //current starsCollected count
    int starsCollected = 0;
    //the start count for stars to be collected
    int starsToCollect;
    void Start()
    {

        SceneManager.activeSceneChanged += ResetValues;
        starsToCollect = stars.transform.childCount;
        UITimeController.OnOutOfTime += OutOfTime;
        CollectableItem.OnStarCollected += StarCollected;
    }
    void Update()
    {
        //if the player goes below -10 meter they would loose due to fall.
        // this could be better done through getting the ground gameobject but since i won't change the height of the ground, it's useless right now.
        if (player.transform.localPosition.y < -10)
        {
            Loose();
        }
    }

    /// <summary>
    /// when the game is lost, the game ended invokes to stop the game and shows the lost canvas.
    /// </summary>
    void Loose()
    {
        OnGameEnded?.Invoke();
        ShowLostCanvas();
    }
    /// <summary>
    /// invoked when a stars is collected 
    /// </summary>
    void StarCollected()
    {
        //increases the ammount of stars collected with 1
        starsCollected++;
        //if the stars collected are equal to the stars start count to be collected
        if (starsCollected == starsToCollect)
        {
            //end the game
            OnGameEnded?.Invoke();
            //show the win canvas
            ShowWinCanvas();
        }
    }

    /// <summary>
    /// invoked when the timer is finished, ends the game and either the player win or loose depending on the stars count.
    /// </summary>
    void OutOfTime()
    {
        //end the game
        OnGameEnded?.Invoke();
        //if the player have earned at least one star, they win, else they loose.
        if (starsCollected > 0)
        {
            ShowWinCanvas();
        }
        else
        {
            ShowLostCanvas();
        }
    }
    /// <summary>
    /// shows Lost canvas by activating it's gameobject.
    /// </summary>
    void ShowLostCanvas()
    {
        loseCanvas.SetActive(true);
    }
    /// <summary>
    /// shows win canvas by activating it's gameobject and sets the star texture for the count the player earned.
    /// </summary>
    void ShowWinCanvas()
    {
        winCanvas.SetActive(true);
        starsUI.GetComponent<RawImage>().texture = starTextures[starsCollected - 1];
    }
    /// <summary>
    /// Resets the value for OnGameEnded event because it's static, when the game ends.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="next"></param>
    void ResetValues(Scene current, Scene next)
    {
        OnGameEnded = null;
    }
}
