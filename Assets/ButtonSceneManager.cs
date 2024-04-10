
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ButtonSceneManager : MonoBehaviour
{
    
    

    /// <summary>
    /// loads a scene based on the textui number of the button(level); a system to load the scene automatically without having to assing scene number manually.
    /// </summary>
    public void LoadLevelByUiTextNumber()
    {
        //the textUi for levelnumber for a button 
        TextMeshProUGUI levelNumberString = transform.GetComponentInChildren<TextMeshProUGUI>();
        //load scene by indexnumber
        SceneManager.LoadSceneAsync(Convert.ToInt32(levelNumberString.text) + 1);
    }


    //when a button to loadscene is clicked, the event for it will call the function to load the scene by name.
    public void ButtonClicked(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    //reloads the scene by event button click.
    public void OnPlayAgainClicked() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    //gets the same active scene and adds 1 to the build index to load the next level.
    public void OnContinueClicked()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //quits the game.
    public void ExitApplication()
    {
        Application.Quit();
    }
}
