using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(PlayerManager.instance.hasKey == false)
            {
                PlayerManager.instance.CollectKey();
                Destroy(gameObject);
            }
        }
    }
}
