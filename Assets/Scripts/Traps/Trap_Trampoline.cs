using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce = 20;

    [SerializeField] private bool canBeUsed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null && canBeUsed)
        {
            canBeUsed = false;
            GetComponent<Animator>().SetTrigger("activate");
            collision.GetComponent<Player>().Push(pushForce);
            AudioManager.instance.PlaySFX(1, transform);
        }
    }

    private void CanUseAgain() => canBeUsed = true;
}
