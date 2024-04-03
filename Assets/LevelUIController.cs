using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
public class LevelUIController : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI levelNumberString;
    

    public void LevelUiButtonClicked()
    {   
         
         levelNumberString = transform.GetComponentInChildren<TextMeshProUGUI>(); 
        SceneManager.LoadSceneAsync(Convert.ToInt32(levelNumberString.text)+1);
    }

    
}
