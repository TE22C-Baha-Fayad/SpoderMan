using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LevelUIGenerator : MonoBehaviour
{

    [SerializeField][Tooltip("the prefab to regenerate")] GameObject LevelButtonUiPrefab;


    void Awake()
    {
        //gets all levels in buildsettings and subtracts 2 which are the Home and Levels scenes.
        int levelsCount = SceneManager.sceneCountInBuildSettings - 2;
        //for every level
        for (int i = 0; i < levelsCount; i++)
        {
            //instanciate button ui prefab
            GameObject levelUIInstance = Instantiate(LevelButtonUiPrefab, transform);
            //name the level in hirarchy
            levelUIInstance.name = $"Level {i}";

            //might do later on
            //levelUIInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (the sprite for stars earned)

            //set the text of the prefab to the level number.
            levelUIInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }

    }



}
