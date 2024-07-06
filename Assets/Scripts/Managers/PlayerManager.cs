using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;        

    [HideInInspector] public int books;
    [HideInInspector] public Transform respawnPoint;
    [HideInInspector] public GameObject currentPlayer;
    [HideInInspector] public int choosenSkinId;
    
    public UI_InGame inGameUI;

    [Header("Items")]    
    public bool hasKey = false;


    [Header("Player Info")]
    [SerializeField] private GameObject bookPrefab;
    [SerializeField] private GameObject playerPrefab;
    public bool playerOnQuiz = false;
    public bool playerIsDead = false;
    public bool playerFinishTheLevel = false;    

    [Header("Camera Shake FX")]
    private CinemachineImpulseSource impulse;
    
    [Header("Camera Shake Value")]
    public float normalMagnitudeShake;
    public float normalTimeShake;    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        impulse = GetComponent<CinemachineImpulseSource>();

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }    

    public void ScreenShake(float intensity, float time)
    {
        impulse.m_ImpulseDefinition.m_AmplitudeGain = intensity;
        impulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = time;
        impulse.GenerateImpulse();
    }

    #region Book
    private bool HaveEnoughBooks()
    {
        if(books > 0)
        {
            books--;

            if(books < 0)
            {
                books = 0;
            }

            DropBook();
            return true;
        }

        return false;
    }

    private void DropBook()
    {
        int bookIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(BookColors)).Length);

        GameObject newBook = Instantiate(bookPrefab, currentPlayer.transform.position, transform.rotation);
        newBook.GetComponent<BookDroppedByPlayer>().BookSetup(bookIndex);
        Destroy(newBook, 20);
    }
    #endregion

    #region Player Condition
    public void OnTakingDamage()
    {
        ScreenShake(normalMagnitudeShake, normalTimeShake);

        if (!HaveEnoughBooks())
        {
            KillPlayer();

            if(GameManager.instance.difficulty < 3)
            {
                Invoke("RespawnPlayer", 1);
            }
            else
            {
                inGameUI.OnDeath();
            }
        }
    }

    public void OnFalling()
    {        
        AudioManager.instance.StopSFX(0);
        KillPlayer();

        int difficulty = GameManager.instance.difficulty;

        if (difficulty < 3)
        {
            Invoke("RespawnPlayer", 1);

            if(difficulty > 1)
            {
                HaveEnoughBooks();
            }
        }
        else
        {
            inGameUI.OnDeath();
        }
    }

    public void RespawnPlayer()
    {
        if(currentPlayer == null)
        {
            playerIsDead = false;
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);                        
        }
    }

    public void KillPlayer()
    {
        playerIsDead = true;
        playerOnQuiz = false;

        if (inGameUI.quizUI.activeInHierarchy)
        {
            inGameUI.quizUI.SetActive(false);
        }

        ScreenShake(normalMagnitudeShake, normalTimeShake);
                
        AudioManager.instance.StopSFX(0); 

        Destroy(currentPlayer, 0.1f);
    }
    #endregion

    #region Key
    public void CollectKey()
    {
        hasKey = true;        
        inGameUI.haveKeyImage.SetActive(true);        
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void RemoveKey()
    {
        hasKey = false;
        inGameUI.haveKeyImage.SetActive(false);
    }
    #endregion
}
