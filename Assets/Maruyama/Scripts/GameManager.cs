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
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private GameObject clearPanel;
    private async UniTaskVoid Start()
    {
        var ct = this.GetCancellationTokenOnDestroy();

        for (int i = 0; i < rounds.Length; i++)
        {
            var round = rounds[i];

            //フェードを待つ
            if(TwoFadeManager.Instance != null)
            {
                await UniTask.WaitUntil(() => !TwoFadeManager.Instance.IsFading);
            }
           
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
            questionUI.ShowCorrectCount(questionInsect, correctCount);

            // SEを鳴らす
            if (answer != correctCount)
            {
                SEManager.Instance.PlayWrong(); // 不正解音
                await stone.MakeTransparency();

                gameOverUI.GameOver(questionInsect, correctCount);
                return;
            }
            else
            {
                SEManager.Instance.PlayCorrect(); // 正解音
            }

            await stone.MakeTransparency();
            
            if (answer != correctCount)
            {
                gameOverUI.GameOver(questionInsect, correctCount);
                return;
            }

            // 次のラウンドへ
            questionUI.HideAnswer();
            await stone.ResetTransparency();
            spawner.Clear();

            // ★最後のラウンド以外はカメラ移動
            if (i < rounds.Length - 1)
            {
                await cameraMove.MoveNextArea();
            }
            else
            {
                // 最後のラウンドならクリアパネル
                if (clearPanel != null)
                    clearPanel.SetActive(true);
            }
        }
    }
}
