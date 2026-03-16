using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    #region 変数とプロパティの宣言
    [SerializeField] private StoneController stone;
    [SerializeField] private NumberInput numberInput;
    [SerializeField] private int totalRounds = 3; // 総ラウンド数　dataのLength
    [SerializeField] private float observeTime = 2f; // これもシリアライズからGetする
    GameManager Instance { get; set; }
    #endregion

    private async UniTaskVoid Start()
    {
        var ct = this.GetCancellationTokenOnDestroy(); // キャンセルトークンを追加
        for (int i = 0; i < totalRounds; i++)
        {
            int correctCount = stone.GetCorrectCount(); // 正解数 Getする
            await stone.PlayOpenAnimation();
            await UniTask.Delay(TimeSpan.FromSeconds(observeTime), cancellationToken: ct);
            await stone.PlayCloseAnimation();

            int answer = await numberInput.WaitForInput();

            await stone.PlayOpenAnimation();

            if (answer != correctCount)
            {
                // ゲームオーバー
                return;
            }

            await stone.PlayCloseAnimation();
        }
        // ゲームクリア
        // SceneManager.LoadScene("ClearScene");
    }
}