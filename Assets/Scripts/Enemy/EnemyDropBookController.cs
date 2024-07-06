using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropBookController : MonoBehaviour
{
    [SerializeField] private GameObject book;

    [Range(1, 5)]
    [SerializeField] private int dropBookAmount;

    public void DropBooks()
    {
        for(int i = 0; i < dropBookAmount; i++)
        {
            GameObject newBook = Instantiate(book, transform.position, transform.rotation);
            Destroy(newBook, 10);
        }
    }
}
