using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;

    [Header("Timer Info")]
    public float timer;
    public bool startTime;

    [Header("Level Info")]
    public int levelNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if(difficulty == 0)
        {
            difficulty = PlayerPrefs.GetInt("GameDifficulty");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(startTime)
        {
            timer += Time.deltaTime;
        }
    }

    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }

    public void SaveBestTime()
    {
        startTime = false;

        float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime", 999);

        if(timer < lastTime)
        {
            PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);
        }

        timer = 0;
    }

    public void SaveCollectedBooks()
    {
        int totalBooks = PlayerPrefs.GetInt("TotalBooksCollected");

        int newTotalBooks = totalBooks + PlayerManager.instance.books;

        PlayerPrefs.SetInt("TotalBooksCollected", newTotalBooks);
        PlayerPrefs.SetInt("Level" + levelNumber + "BooksCollected", PlayerManager.instance.books);

        PlayerManager.instance.books = 0;
    }

    public void SaveLevelInfo()
    {
        int nextLevelNumber = levelNumber + 1;
        PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked", 1);
    }
}
