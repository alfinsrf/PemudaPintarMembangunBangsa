using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();

            player.Knockback(transform);
        }                   
    }
}
