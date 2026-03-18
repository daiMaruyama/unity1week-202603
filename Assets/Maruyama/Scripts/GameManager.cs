using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲーム全体の進行を管理するMediator
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private StoneController stone;
    [SerializeField] private NumberInput numberInput;
    [SerializeField] private InsectSpawn spawner;
    [SerializeField] private RoundData[] rounds;

    private async UniTaskVoid Start()
    {
        var ct = this.GetCancellationTokenOnDestroy(); // 安全に非同期処理をキャンセルするためのトークン

        for (int i = 0; i < rounds.Length; i++)
        {
            var round = rounds[i];

            // 虫を配置
            spawner.SpawnInsects(round.min, round.max, round.randomTypeCount);
            Debug.Log($"ラウンド {i + 1}: 虫を配置しました。");

            // 観察フェーズ
            await stone.PlayOpenAnimation();
            await UniTask.Delay(TimeSpan.FromSeconds(round.observeTime), cancellationToken: ct);
            await stone.PlayCloseAnimation();

            // 出題
            spawner.PickQuestion();
            int correctCount = spawner.GetCorrectCount();
            Debug.Log($"問題：{spawner.GetQuestionInsect().insectName}、正解数：{correctCount}");

            // 回答待ち
            int answer = await numberInput.WaitForInput();

            // 答え合わせ
            await stone.MakeTransparency();

            if (answer != correctCount)
            {
                Debug.Log("不正解！ゲームオーバー");
                return; // 1ミス即終了
            }

            // 次のラウンドへ
            Debug.Log(i == rounds.Length - 1 ? "全てのラウンドをクリア！おめでとう！" : "正解！次のラウンドへ！");
            await stone.ResetTransparency();
            spawner.Clear();
        }
    }
}