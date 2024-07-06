using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI collectedBooks;    

    public void UpdateTextInfo(int levelNumber)
    {
        levelName.text = "Level " + levelNumber;
        bestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime", 999).ToString("00") + " seconds";
        collectedBooks.text = "You collect: " + PlayerPrefs.GetInt("Level" + levelNumber + "BooksCollected", PlayerManager.instance.books).ToString();        
    }
}
