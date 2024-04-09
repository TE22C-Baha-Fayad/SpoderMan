
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneManager : MonoBehaviour
{

    //simplify into one function with parameter
    public void ButtonClicked(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void OnPlayAgainClicked() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnContinueClicked()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
