using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UITimeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int levelTimeInSeconds;
    private TextMeshProUGUI timeLeftUi;
    public delegate void OutOfTime();
    public static event OutOfTime onOutOfTime;

    string timeLeftUiStartValue;
    float levelTimefloat;
    void Start()
    {
        levelTimefloat = (float)levelTimeInSeconds;
        timeLeftUi = GetComponent<TextMeshProUGUI>();
        timeLeftUiStartValue = timeLeftUi.text;
    }

    // Update is called once per frame

    void Update()
    {
        SceneManager.activeSceneChanged += ResetValues;
        if (levelTimeInSeconds > 0)
            levelTimefloat -= Time.deltaTime;

        levelTimeInSeconds = (int)levelTimefloat;

        if (levelTimeInSeconds == 0)
            onOutOfTime?.Invoke();

        timeLeftUi.text = timeLeftUiStartValue + "<color=#262899>" + levelTimeInSeconds + "</color><color=#20187d>S</color>";
    }
    void ResetValues(Scene current, Scene next)
    {
        onOutOfTime = null;
    }


}
