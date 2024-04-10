using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UITimeController : MonoBehaviour
{
    [SerializeField] int levelTimeInSeconds; // current level time in seconds
    private TextMeshProUGUI timeLeftUi; // the label for time left
    string timeLeftUiStartValue; // the start value for the label to later add the time to the text

    //delegate for when the player is outoftime
    public delegate void OutOfTime();
    public static event OutOfTime OnOutOfTime;

    
    float levelTimefloat; // level time in float to subtract time.deltatime from it 
    bool gameEnded = false; // game ended state
    void Start()
    {
        //subscribing to the events
        CanvasController.OnGameEnded += StopTimer;
        SceneManager.activeSceneChanged += ResetValues;
        levelTimefloat = levelTimeInSeconds;
        timeLeftUi = GetComponent<TextMeshProUGUI>();
        timeLeftUiStartValue = timeLeftUi.text;
    }

    // Update is called once per frame

    void Update()
    {
        //while the game isn't ended
        if (!gameEnded)
        {
            //if the level time in seconds which is displayed is more than 0
            if (levelTimeInSeconds > 0)
                levelTimefloat -= Time.deltaTime; // subtract time.delta time to count down.
            levelTimeInSeconds = (int)levelTimefloat; // assign leveltimeinseconds to the the float value converted to int.
            if (levelTimeInSeconds == 0)// if the time is 0
                OnOutOfTime?.Invoke();// invoke out of time.

            timeLeftUi.text = timeLeftUiStartValue + "<color=#262899>" + levelTimeInSeconds + "</color><color=#20187d> S</color>"; // assignt the value and change some colors in html
        }

    }
    //stops the timer by setting the gameended boolean to true, invoked by ongameended event
    void StopTimer()
    {
        gameEnded = true;
    }
    //resets the static values to not be saved if scenen changes.
    void ResetValues(Scene current, Scene next)
    {
        OnOutOfTime = null;
    }


}
