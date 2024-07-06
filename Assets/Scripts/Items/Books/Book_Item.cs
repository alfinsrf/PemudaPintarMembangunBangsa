using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookColors
{
    brown
}

public class Book_Item : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer sr;
    public BookColors myBookColors;

    [SerializeField] private Sprite[] bookImage;    

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.books++;                  

            Destroy(gameObject);
        }
    }

    public void BookSetup(int bookIndex)
    {
        for(int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 1);
        }

        anim.SetLayerWeight(bookIndex, 1);
    }
}
