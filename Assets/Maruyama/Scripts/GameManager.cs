using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    #region 変数とプロパティの宣言
    [SerializeField] private StoneController stone;
    [SerializeField] private NumberInput numberInput;
    [SerializeField] private int totalRounds = 3; // 総ラウンド数　dataのLength
    [SerializeField] private float observeTime = 2f; // これもシリアライズからGetする
    #endregion

    private async UniTaskVoid Start()
    {
        var ct = this.GetCancellationTokenOnDestroy(); // キャンセルトークンを追加
        for (int i = 0; i < totalRounds; i++)
        {
            // int correctCount = stone.GetCorrectCount(); // 正解数 Getする
            int correctCount = UnityEngine.Random.Range(0, 100); // とりあえずランダムで
            Debug.Log($"Round {i + 1}: Correct Count = {correctCount}"); // デバッグ用ログ
            await stone.PlayOpenAnimation();
            await UniTask.Delay(TimeSpan.FromSeconds(observeTime), cancellationToken: ct);
            await stone.PlayCloseAnimation();

            int answer = await numberInput.WaitForInput();

            await stone.MakeTransparency();

            if (answer != correctCount)
            {
                // ゲームオーバー
                Debug.Log("Game Over!");
                return;
            }
            await stone.ResetTransparency();
        }
        // ゲームクリア
        // SceneManager.LoadScene("ClearScene");
    }
}