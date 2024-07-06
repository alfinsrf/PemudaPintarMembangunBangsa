using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuiz : MonoBehaviour
{   
    public QuizManager quizManager;

    public Collider2D enemyTriggerCollider;
    public Collider2D enemyCollider;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {            
            quizManager.LoadNewQuestion(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            quizManager.quizUI.SetActive(false);
            PlayerManager.instance.playerOnQuiz = false;
        }
    }    
}
