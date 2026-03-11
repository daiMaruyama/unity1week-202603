using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    /// <summary>
    /// シーン遷移ユーティリティ
    /// </summary>
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        public bool IsLoading { get; private set; }

        public event Action OnLoadStart;
        public event Action OnLoadComplete;

        public void LoadScene(string sceneName)
        {
            if (IsLoading) return;
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            IsLoading = true;
            OnLoadStart?.Invoke();

            var op = SceneManager.LoadSceneAsync(sceneName);
            while (!op.isDone)
            {
                yield return null;
            }

            IsLoading = false;
            OnLoadComplete?.Invoke();
        }
    }
}
