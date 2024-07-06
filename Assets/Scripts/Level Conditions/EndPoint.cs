using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private UI_InGame inGame_UI;

    // Start is called before the first frame update
    private void Start()
    {
        inGame_UI = GameObject.Find("Canvas").GetComponent<UI_InGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.LevelFinished();                                                    

            inGame_UI.OnLevelFinished();

            GameManager.instance.SaveBestTime();
            GameManager.instance.SaveCollectedBooks();
            GameManager.instance.SaveLevelInfo();
        }
    }
}
