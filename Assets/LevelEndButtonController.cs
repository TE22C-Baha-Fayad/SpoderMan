using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndButtonController : MonoBehaviour
{


    public void OnHomeClicked()
    {
        SceneManager.LoadSceneAsync("Home");
    }
    public void OnLevelsClicked()
    {
        SceneManager.LoadSceneAsync("Levels");
    }
    public void OnPlayAgainClicked()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnContinueClicked()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
