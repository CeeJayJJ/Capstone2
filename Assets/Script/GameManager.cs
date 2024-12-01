using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Question[] questions; // Array of questions
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField] private Text questionText; // Question text UI
    [SerializeField] private Button optionAButton;
    [SerializeField] private Button optionBButton;
    [SerializeField] private Text optionAText;
    [SerializeField] private Text optionBText;

    [SerializeField] private Text timerText; // Timer UI
    private float timer = 10f; // Timer for each question
    private bool isQuizActive = true;

    private int correctAnswers = 0; // Score tracking
    private int totalQuestions = 5;

    [SerializeField] private GameObject quizPanel; // Quiz panel
    [SerializeField] private GameObject resultPanel; // Results panel
    [SerializeField] private Text resultText; // Result text UI
    [SerializeField] private Text scoreText; // New: Score Text UI for displaying the score

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = new List<Question>(questions);
        }

        SetCurrentQuestion();
        StartCoroutine(Timer());
    }

    void SetCurrentQuestion()
    {
        if (unansweredQuestions.Count == 0)
        {
            EndQuiz();
            return;
        }

        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        unansweredQuestions.Remove(currentQuestion);

        // Display the question and options
        questionText.text = currentQuestion.fact;
        optionAText.text = currentQuestion.optionA;
        optionBText.text = currentQuestion.optionB;
    }

    public void UserSelectOptionA()
    {
        if (currentQuestion.isOptionACorrect)
        {
            correctAnswers++;
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectOptionB()
    {
        if (!currentQuestion.isOptionACorrect)
        {
            correctAnswers++;
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    IEnumerator TransitionToNextQuestion()
    {
        yield return new WaitForSeconds(1f); // Short delay before loading the next question
        SetCurrentQuestion();
        ResetTimer();
    }

    IEnumerator Timer()
    {
        while (isQuizActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = Mathf.Ceil(timer).ToString();
            }
            else
            {
                timerText.text = "Time's up!";
                EndQuiz();
            }
            yield return null;
        }
    }

    void ResetTimer()
    {
        timer = 10f; // Reset the timer for the next question
    }

    void EndQuiz()
    {
        isQuizActive = false;
        quizPanel.SetActive(false); // Hide the quiz panel
        resultPanel.SetActive(true); // Show the result panel

        // Update the score text
        scoreText.text = $"{correctAnswers}/{totalQuestions}"; // Display player's score

        // Generate detailed feedback based on the score
        string feedback;
        if (correctAnswers == 5)
        {
            feedback = "Excellent! You’re a highly responsible tech user! Keep up the great work maintaining healthy tech habits.";
        }
        else if (correctAnswers >= 3)
        {
            feedback = "Good job! You’re on your way to mastering responsible tech habits. Reflect on some of your choices to improve further.";
        }
        else if (correctAnswers >= 1)
        {
            feedback = "Needs improvement! Remember to prioritize balance and make thoughtful decisions about technology use.";
        }
        else
        {
            feedback = "Oops! This is a wake-up call to rethink your technology habits. Balance is key to a healthier, more productive lifestyle.";
        }

        resultText.text = feedback; // Show feedback only, without the score
    }
}
