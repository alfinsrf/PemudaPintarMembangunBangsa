using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPoint : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            canvas.SetActive(true);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {            
            canvas.SetActive(false);
        }
    }
}
