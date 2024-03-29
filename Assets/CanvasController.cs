using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject starsCanvasGameObject;
    [SerializeField] Texture[] starTextures;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject LoseCanvas;

    
    int starsCollected = 0;
    void Start()
    {
        UITimeController.onOutOfTime += OutOfTime;
        CollectableItem.onStarCollected += StarCollected;
    }

    void StarCollected()
    {
        starsCollected++;
        if(starsCollected == 3)
        {
            ShowWinCanvas();
        }
    }
    void OutOfTime()
    {
        if(starsCollected>0)
        {
            ShowWinCanvas();
        }
        else{
            ShowLoseCanvas();
        }
    }

    void ShowLoseCanvas()
    {
        LoseCanvas.SetActive(true);
    }
    void ShowWinCanvas()
    {
        winCanvas.SetActive(true);
        starsCanvasGameObject.GetComponent<RawImage>().texture = starTextures[starsCollected-1];

    }
}
