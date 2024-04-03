using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UITimeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int levelTimeInSeconds;
    private TextMeshProUGUI timeLeftUi;
    public delegate void OutOfTime();
    public static event OutOfTime OnOutOfTime;

    string timeLeftUiStartValue;
    float levelTimefloat;
    bool gameEnded = false;
    void Start()
    {
        CanvasController.OnGameEnded += GameEnded;
        SceneManager.activeSceneChanged += ResetValues;
        levelTimefloat = levelTimeInSeconds;
        timeLeftUi = GetComponent<TextMeshProUGUI>();
        timeLeftUiStartValue = timeLeftUi.text;
    }

    // Update is called once per frame

    void Update()
    {

        if (!gameEnded)
        {
            if (levelTimeInSeconds > 0)
                levelTimefloat -= Time.deltaTime;

            levelTimeInSeconds = (int)levelTimefloat;

            if (levelTimeInSeconds == 0)
                OnOutOfTime?.Invoke();

            timeLeftUi.text = timeLeftUiStartValue + "<color=#262899>" + levelTimeInSeconds + "</color><color=#20187d>S</color>";
        }

    }
    void GameEnded()
    {
        gameEnded = true;
    }
    void ResetValues(Scene current, Scene next)
    {
        OnOutOfTime = null;
    }


}
