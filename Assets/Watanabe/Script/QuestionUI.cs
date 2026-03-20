using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    [Header("正解表示用UI")]
    [SerializeField] private GameObject answerPanel;       // パネル全体
    [SerializeField] private TextMeshProUGUI insectText;   // テキスト
    [SerializeField] private Image insectImage;            // 虫の画像
    [SerializeField] private GameObject problemText;
    [SerializeField] private GameObject answerText;

    /// <summary>
    /// 問題の文字とイメージをだす
    /// 問題出題時に呼ぶ
    /// </summary>
    public void ShowQuestionInsect(InsectData insect)
    {
        if (insect == null) return;

        answerPanel.SetActive(true);
        insectText.text = $"{insect.insectName}は何匹？";
        insectImage.sprite = insect.icon;
        if (problemText != null) problemText.SetActive(true);
        if (answerText != null) answerText.SetActive(false);
    }

    /// <summary>
    /// 答えの数
    /// 答え合わせ時に呼ぶ
    /// </summary>
    public void ShowCorrectCount(InsectData insect, int correctCount)
    {
        if (insect == null) return;

        answerPanel.SetActive(true);
        insectText.text = $"{insect.insectName}は{correctCount}匹！";
        insectImage.sprite = insect.icon;
        if (problemText != null) problemText.SetActive(false);
        if (answerText != null) answerText.SetActive(true);
    }

    /// <summary>
    /// 非表示
    /// 次のラウンド前などに呼ぶ
    /// </summary>
    public void HideAnswer()
    {
        answerPanel.SetActive(false);
    }
}
