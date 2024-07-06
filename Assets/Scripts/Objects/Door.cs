using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;    
    private bool isOpen = false;
    [SerializeField] private Collider2D doorCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null && PlayerManager.instance.HasKey())
        {            
            PlayerManager.instance.RemoveKey();
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if(!isOpen)
        {
            isOpen = true;
            anim.SetTrigger("open");
            doorCollider.enabled = false;            
        }
    }
}
