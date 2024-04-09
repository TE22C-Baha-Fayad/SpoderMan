using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject starsUI;
    [SerializeField] Texture[] starTextures;

    [SerializeField] GameObject player;
    [SerializeField] GameObject stars;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    public delegate void GameEnded();
    public static event GameEnded OnGameEnded;


    int starsCollected = 0;
    int starsStartCount;
    void Start()
    {
        SceneManager.activeSceneChanged += ResetValues;
        starsStartCount = stars.transform.childCount;
        UITimeController.OnOutOfTime += OutOfTime;
        CollectableItem.OnStarCollected += StarCollected;
    }
    void Update()
    {
        if(player.transform.localPosition.y < -10)
        {
            OnGameEnded?.Invoke();
            ShowLoseCanvas();
        }
    }

    void StarCollected()
    {
        starsCollected++;
        if (starsCollected == starsStartCount)
        {
            OnGameEnded?.Invoke();
            ShowWinCanvas();
        }
    }
    void OutOfTime()
    {
        OnGameEnded?.Invoke();
        if (starsCollected > 0)
        {
            ShowWinCanvas();
        }
        else
        {
            ShowLoseCanvas();
        }
    }

    void ShowLoseCanvas()
    {
        loseCanvas.SetActive(true);
    }
    void ShowWinCanvas()
    {
        winCanvas.SetActive(true);
        starsUI.GetComponent<RawImage>().texture = starTextures[starsCollected - 1];

    }
    void ResetValues(Scene current, Scene next)
    {
        OnGameEnded = null;
    }
}
