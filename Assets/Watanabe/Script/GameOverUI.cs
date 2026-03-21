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

    public async void OnStartButton()
    {
        SEManager.Instance.PlayButton();
        await Cysharp.Threading.Tasks.UniTask.Delay(200);
        TwoFadeManager.Instance.StartFadeOutAndLoadScene("Title"); // ゲームシーン名に置き換え
    }

    public async void OnRestartButton()
    {
        SEManager.Instance.PlayButton();
        await Cysharp.Threading.Tasks.UniTask.Delay(200);
        TwoFadeManager.Instance.StartFadeOutAndLoadScene("GameScene");
    }
}
