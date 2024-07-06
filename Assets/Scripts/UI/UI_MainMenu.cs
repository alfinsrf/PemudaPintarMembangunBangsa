using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{    
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_VolumeController[] volumeController;
    

    // Start is called before the first frame update
    private void Start() 
    {        
        bool showButton = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(showButton);

        for(int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_VolumeController>().SetupVolumeSlider();
        }

        AudioManager.instance.PlayBGM(0);                
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXUI(3);
    }

    public void SwitchMenuTo(GameObject uiMenu)
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

    public void SetGameDifficulty(int i) => GameManager.instance.difficulty = i;    

    public void QuitGame()
    {        
        Application.Quit();
    }
}
