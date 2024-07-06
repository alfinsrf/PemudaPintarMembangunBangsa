using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private bool[] levelOpen;
    [SerializeField] private UI_DarkScreen darkScreen;

    private void Awake()
    {
        darkScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);

        AssignLevelBooleans();        

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i])
            {
                return;
            }

            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(LoadLevel(sceneName)));
            newButton.GetComponent<UI_LevelButton>().UpdateTextInfo(i);
        }
    }

    private void AssignLevelBooleans()
    {
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(unlocked)
            {
                levelOpen[i] = true;
            }
            else
            {
                return;
            }
        }
    }

    public IEnumerator LoadLevel(string sceneName)
    {        
        GameManager.instance.SaveGameDifficulty();

        darkScreen.FadeOut();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName);
    }

    public void LoadNewGame()
    {
        GameManager.instance.SaveGameDifficulty();
        StartCoroutine(LoadNewGameCoroutine());
    }

    IEnumerator LoadNewGameCoroutine()
    {        
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(unlocked)
            {
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            }
            else
            {
                darkScreen.FadeOut();                

                yield return new WaitForSeconds(3f);

                SceneManager.LoadScene("Level 1");
            }
        }
    }

    public void LoadContinueGame()
    {
        StartCoroutine(LoadContinueGameCoroutine());
    }

    IEnumerator LoadContinueGameCoroutine()
    {        
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(!unlocked)
            {
                darkScreen.FadeOut();

                yield return new WaitForSeconds(3f);

                SceneManager.LoadScene("Level " + (i - 1));                
            }
        }
    }
}
