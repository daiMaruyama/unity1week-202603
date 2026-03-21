using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Core;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] float _fadeDuration = 1.0f;
    bool _isFading = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // シーン読み込み完了イベントにメソッドを登録
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // オブジェクトが消えるときは登録を解除
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _isFading = false;
        StopAllCoroutines(); // 前のフェードが動いていたら止めて、新しくフェードインを開始
        StartCoroutine(FadeIn());
    }

    public void StartFadeOutAndLoadScene(string sceneName) // ボタン
    {
        if(_isFading)
        {
            return;
        }
        StartCoroutine(FadeAndLeave(sceneName));
    }

    private IEnumerator FadeAndLeave(string sceneName)
    {
        _isFading = true;

        float timer = 0;
        _fadeCanvasGroup.blocksRaycasts = true;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _fadeCanvasGroup.alpha = timer / _fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        _isFading = true;

        float timer = _fadeDuration;
        _fadeCanvasGroup.alpha = 1; // 真っ黒からスタート
        _fadeCanvasGroup.blocksRaycasts = true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            _fadeCanvasGroup.alpha = timer / _fadeDuration;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 0;
        _fadeCanvasGroup.blocksRaycasts = false; // 終わったらクリックを通す

        _isFading = false;
    }
}
