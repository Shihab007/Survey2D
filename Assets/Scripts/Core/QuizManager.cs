using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("Question Display")]
    public Image questionImage;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI questionCounterText;

    [Header("Answer Buttons")]
    public Button[] answerButtons;
    public Image[] answerImages;
    public TextMeshProUGUI[] answerLabels;

    [Header("Feedback")]
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI explanationText;
    public Button nextButton;

    [Header("Quiz Data")]
    public QuizQuestionSO[] questions;

    private int currentIndex = 0;
    private bool answered = false;

    void Start()
    {
        feedbackPanel.SetActive(false);
        LoadQuestion(currentIndex);
    }

    void LoadQuestion(int index)
    {
        answered = false;
        feedbackPanel.SetActive(false);

        QuizQuestionSO q = questions[index];

        questionImage.sprite = q.questionImage;
        questionText.text = q.questionText;
        questionCounterText.text = (index + 1) + " / " + questions.Length;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerImages[i].sprite = q.answerImages[i];
            answerLabels[i].text = q.answerLabels[i];
            answerButtons[i].interactable = true;

            int capturedIndex = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(capturedIndex));
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (answered) return;
        answered = true;

        QuizQuestionSO q = questions[currentIndex];

        foreach (Button btn in answerButtons)
            btn.interactable = false;

        if (selectedIndex == q.correctAnswerIndex)
        {
            GameManager.Instance.AddScore();
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            explanationText.text = "";
        }
        else
        {
            feedbackText.text = "Incorrect!";
            feedbackText.color = Color.red;
            explanationText.text = q.explanationText;
        }

        feedbackPanel.SetActive(true);
    }

    public void OnNextButton()
    {
        currentIndex++;

        if (currentIndex < questions.Length)
        {
            LoadQuestion(currentIndex);
        }
        else
        {
            GameManager.Instance.LoadScene("ResultScreen");
        }
    }
}