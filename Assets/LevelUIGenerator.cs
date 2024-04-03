using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LevelUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LevelButtonUiPrefab;
    
    int levelsCount;
    void Awake()
    {
        levelsCount = SceneManager.sceneCountInBuildSettings - 2;
        for(int i = 0; i<levelsCount; i++)
        {
            GameObject levelUIInstance = Instantiate(LevelButtonUiPrefab, transform);
            levelUIInstance.name = $"Level {i}";
            // levelUIInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (the sprite for stars earned)
            levelUIInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
        }

    }
    
   
  
}
