using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_InGame : MonoBehaviour
{
    private bool gamePaused;

    [Header("Menu Game Objects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject endLevelUI;
    public GameObject quizUI;
    [SerializeField] private UI_DarkScreen darkScreen;

    [Header("Lose Level Components")]
    private bool playerLoseTheLevel = false;    
    [SerializeField] private GameObject loseButtonHandler;

    [Header("End Level Components")]
    [SerializeField] private GameObject textCongrats;
    [SerializeField] private GameObject textYourTime;
    [SerializeField] private GameObject textBestTime;
    [SerializeField] private GameObject textYourBooks;
    [SerializeField] private GameObject ButtonEndLevelHandler;    

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentPlayerBooks;

    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI endBestTimeText;
    [SerializeField] private TextMeshProUGUI endBooksText;

    [Header("Image Components")]
    public GameObject haveKeyImage;

    [Header("Volume Controller")]
    [SerializeField] private UI_VolumeController[] volumeController;

    private void Awake()
    {
        PlayerManager.instance.inGameUI = this;

        darkScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;

        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_VolumeController>().SetupVolumeSlider();
        }

        haveKeyImage.SetActive(false);
        SwitchUI(inGameUI);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInGameInfo();
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (endLevelUI.activeInHierarchy || loseUI.activeInHierarchy)
            {
                return;
            }

            CheckIfNotPaused();
        }
        
        if(endLevelUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }
        
        if (playerLoseTheLevel == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReloadCurrentLevel();
            }
        }
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXUI(3);
    }    

    public void PauseButton() => CheckIfNotPaused();

    private bool CheckIfNotPaused()
    {
        if(!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    public void OnDeath() => SwitchToLoseScreen();

    public void OnLevelFinished()
    {
        endTimerText.text = "Your time: " + GameManager.instance.timer.ToString("00") + " seconds";
        endBestTimeText.text = "Best time for this level: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime", 999).ToString("00") + " seconds";
        endBooksText.text = "You collect: " + PlayerManager.instance.books;

        SwitchUI(endLevelUI);        
        StartCoroutine(EndLevelCoroutine());
    }

    IEnumerator EndLevelCoroutine()
    {                                     
        textCongrats.SetActive(false);
        textYourTime.SetActive(false);
        textBestTime.SetActive(false);
        textYourBooks.SetActive(false);
        ButtonEndLevelHandler.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        textCongrats.SetActive(true);

        yield return new WaitForSeconds(1f);
        textYourTime.SetActive(true);

        yield return new WaitForSeconds(1f);
        textBestTime.SetActive(true);

        yield return new WaitForSeconds(1f);
        textYourBooks.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        ButtonEndLevelHandler.SetActive(true);
    }

    private void UpdateInGameInfo()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " seconds";
        currentPlayerBooks.text = PlayerManager.instance.books.ToString();
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            bool darkScreen = transform.GetChild(i).GetComponent<UI_DarkScreen>() != null;

            if (darkScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }            
        }
        
        uiMenu.SetActive(true);        
    }

    public void SwitchToLoseScreen()
    {        
        StartCoroutine(LoseScreenCoroutine());
    }

    IEnumerator LoseScreenCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        darkScreen.FadeOut();       

        yield return new WaitForSeconds(3f);
        loseUI.SetActive(true);
        loseButtonHandler.SetActive(true);
        playerLoseTheLevel = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        darkScreen.FadeOut();

        StartCoroutine(LoadMainMenuCoroutine());
    }

    IEnumerator LoadMainMenuCoroutine()
    {        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void LoadNextLevel() 
    {
        darkScreen.FadeOut();
        StartCoroutine(LoadNextLevelCoroutine());
    }

    IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(3f);        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
}
