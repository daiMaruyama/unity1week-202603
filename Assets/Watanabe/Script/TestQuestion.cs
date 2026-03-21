using UnityEngine;

public class TestQuestion : MonoBehaviour
{
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private InsectSpawn spawner;
    [SerializeField] private GameOverUI gameOverUI;

    // 問題の虫を表示するボタン用
    public void OnShowQuestionButton()
    {
        var questionInsect = spawner.GetQuestionInsect();
        questionUI.ShowQuestionInsect(questionInsect);
    }

    // 答え合わせの虫の数を表示するボタン用
    public void OnShowAnswerButton()
    {
        var questionInsect = spawner.GetQuestionInsect();
        if (questionInsect == null) return;

        int correctCount = spawner.CountInsect(questionInsect);
        questionUI.ShowCorrectCount(questionInsect, correctCount);
    }

    public void OnHideButton()
    {
        questionUI.HideAnswer();
    }

    public void OnGameoverButton()
    {
        var questionInsect = spawner.GetQuestionInsect();
        if (questionInsect == null) return;

        int correctCount = spawner.CountInsect(questionInsect);
        gameOverUI.GameOver(questionInsect, correctCount);
    }
}
