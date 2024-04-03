using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject starsCanvasGameObject;
    [SerializeField] Texture[] starTextures;

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
        starsCanvasGameObject.GetComponent<RawImage>().texture = starTextures[starsCollected - 1];

    }
    void ResetValues(Scene current, Scene next)
    {
        OnGameEnded = null;
    }
}
