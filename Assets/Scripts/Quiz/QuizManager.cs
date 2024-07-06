using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject quizUI;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    public List<Question> questionsList;
    private Question currentQuestion;

    private GameObject currentEnemy;
       
    // Start is called before the first frame update
    void Start()
    {
        quizUI.SetActive(false);        
    }    

    public void LoadNewQuestion(GameObject _enemy)
    {
        currentEnemy = _enemy;
        quizUI.SetActive(true);
        PlayerManager.instance.playerOnQuiz = true;

        if (questionsList.Count > 0)
        {
            int randomIndex = Random.Range(0, questionsList.Count);
            currentQuestion = questionsList[randomIndex];

            questionText.text = currentQuestion.questionText;
            questionText.gameObject.SetActive(true);

            List<int> answerIndices = new List<int> { 0, 1, 2, 3 };
            answerIndices = ShuffleQuestionList(answerIndices);            

            for(int i = 0; i < answerButtons.Length; i++)
            {
                if(i < currentQuestion.answers.Length)
                {
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[answerIndices[i]];
                    answerButtons[i].gameObject.SetActive(true);

                    answerButtons[i].onClick.RemoveAllListeners();

                    bool isCorrect = (answerIndices[i] == currentQuestion.correctAnswerIndex);
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(isCorrect));
                }                
            }            
        }
        else
        {            
            quizUI.SetActive(false);
            PlayerManager.instance.playerOnQuiz = false;            
        }
    }

    public void OnAnswerSelected(bool isCorrect)
    {
        if(isCorrect)
        {                 
            quizUI.gameObject.SetActive(false);
            PlayerManager.instance.playerOnQuiz = false;

            if (currentEnemy != null)
            {                
                currentEnemy.GetComponent<Animator>().SetTrigger("Dead");
                currentEnemy.GetComponent<EnemyQuiz>().enemyTriggerCollider.enabled = false;
                currentEnemy.GetComponent<EnemyQuiz>().enemyCollider.enabled = false;

                if(currentEnemy.GetComponent<EnemyDropBookController>() != null)
                {
                    currentEnemy.GetComponent<EnemyDropBookController>().DropBooks();
                }

                Destroy(currentEnemy, 2);
            }
        }
        else
        {                    
            quizUI.gameObject.SetActive(false);
            PlayerManager.instance.playerOnQuiz = false;
            PlayerManager.instance.OnTakingDamage();
        }        
    }

    List<int> ShuffleQuestionList(List<int> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
