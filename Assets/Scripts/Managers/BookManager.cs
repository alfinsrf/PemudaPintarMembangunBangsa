using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BookManager : MonoBehaviour
{
    [SerializeField] private Transform[] bookPosition;
    [SerializeField] private GameObject bookPrefab;
    [SerializeField] private bool randomBooks;

    private int bookIndex;

    // Start is called before the first frame update
    void Start()
    {
        bookPosition = GetComponentsInChildren<Transform>();

        for(int i = 1; i < bookPosition.Length; i++)
        {
            GameObject newBooks = Instantiate(bookPrefab, bookPosition[i]);

            if(randomBooks)
            {
                bookIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(BookColors)).Length);
                newBooks.GetComponent<Book_Item>().BookSetup(bookIndex);
            }
            else
            {
                newBooks.GetComponent<Book_Item>().BookSetup(bookIndex);
                bookIndex++;

                if(bookIndex > Enum.GetNames(typeof(BookColors)).Length)
                {
                    bookIndex = 0;
                }
            }

            bookPosition[i].GetComponent<SpriteRenderer>().sprite = null;

            int levelNumber = GameManager.instance.levelNumber;
            int totalAmountOfBooks = PlayerPrefs.GetInt("Level" + levelNumber + "TotalBooks");

            if(totalAmountOfBooks != bookPosition.Length -1)
            {
                PlayerPrefs.SetInt("Level" + levelNumber + "TotalBooks", bookPosition.Length - 1);
            }
        }
    }    
}
