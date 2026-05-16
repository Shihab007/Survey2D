using UnityEngine;

[CreateAssetMenu(fileName = "QuizQuestion", menuName = "HeritageQuiz/Quiz Question")]
public class QuizQuestionSO : ScriptableObject
{
    [Header("Question")]
    public Sprite questionImage;
    public string questionText;

    [Header("Answer Options")]
    public Sprite[] answerImages = new Sprite[4];
    public string[] answerLabels = new string[4];

    [Header("Correct Answer")]
    [Range(0, 3)]
    public int correctAnswerIndex;

    [Header("Feedback")]
    public string explanationText;
}