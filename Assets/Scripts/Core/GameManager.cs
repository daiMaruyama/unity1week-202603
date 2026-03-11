using UnityEngine;

namespace Core
{
    /// <summary>
    /// ゲーム全体の状態管理
    /// </summary>
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public enum GameState
        {
            Title,
            InGame,
            Pause,
            Result
        }

        public GameState CurrentState { get; private set; } = GameState.Title;

        public void ChangeState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"[GameManager] State changed to {newState}");
        }
    }
}
