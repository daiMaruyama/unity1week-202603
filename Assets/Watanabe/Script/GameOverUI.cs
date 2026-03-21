using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI answerText; // 正解の虫と数用

    private void Start()
    {
        panel.SetActive(false);
    }

    public void GameOver(InsectData insect, int correctCount)
    {
        panel.SetActive(true);
        if (answerText != null && insect != null)
            answerText.text = $"答え\n{insect.insectName}が{correctCount}匹！";
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Title"); // ゲームシーン名に置き換え
    }

    public void OnResturtButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
