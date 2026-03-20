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
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private RoundUI roundUI;
    private async UniTaskVoid Start()
    {
        var ct = this.GetCancellationTokenOnDestroy();

        for (int i = 0; i < rounds.Length; i++)
        {
            var round = rounds[i];

            // ラウンド表示
            roundUI.UpdateRound(i + 1, rounds.Length);

            // 虫を配置
            spawner.SpawnInsects(round.min, round.max, round.randomTypeCount);

            // 観察フェーズ
            await stone.PlayOpenAnimation();
            await UniTask.Delay(TimeSpan.FromSeconds(round.observeTime), cancellationToken: ct);
            await stone.PlayCloseAnimation();

            // 出題
            spawner.PickQuestion();
            int correctCount = spawner.GetCorrectCount();
            InsectData questionInsect = spawner.GetQuestionInsect();
            questionUI.ShowQuestionInsect(questionInsect);

            // 回答待ち
            int answer = await numberInput.WaitForInput();

            // 答え合わせ
            await stone.MakeTransparency();
            questionUI.ShowCorrectCount(questionInsect, correctCount);

            if (answer != correctCount)
            {
                gameOverUI.GameOver(questionInsect, correctCount);
                return;
            }

            // 次のラウンドへ
            questionUI.HideAnswer();
            await stone.ResetTransparency();
            spawner.Clear();
        }
    }
}