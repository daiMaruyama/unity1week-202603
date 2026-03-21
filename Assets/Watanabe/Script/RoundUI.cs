using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    public TMP_Text roundText;
    public GameManager gameManager;

    // 例えば GameManager からイベントで更新させる
    public void UpdateRound(int current, int total)
    {
        roundText.text = $"Round {current} / {total}";
    }
}
