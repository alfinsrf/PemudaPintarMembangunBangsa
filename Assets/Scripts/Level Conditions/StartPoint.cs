using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;    

    // Start is called before the first frame update
    private void Start()
    {
        PlayerManager.instance.respawnPoint = respawnPoint;
        PlayerManager.instance.RespawnPlayer();

        PlayerManager.instance.books = 0;
        GameManager.instance.timer = 0;

        AudioManager.instance.PlayRandomBGM();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(!GameManager.instance.startTime)
            {
                GameManager.instance.startTime = true;
            }            
        }
    }
}
