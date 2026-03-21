using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Core;

public class TwoFadeManager : SingletonMonoBehaviour<TwoFadeManager>
{
    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] float _fadeDuration = 1.0f;

    bool _isFading = false;
    public bool IsFading => _isFading;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartFadeOutAndLoadScene(string sceneName)
    {
        //if (_isFading) return;
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

        _isFading = false;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _isFading = true;

        float timer = _fadeDuration;
        _fadeCanvasGroup.alpha = 1;
        _fadeCanvasGroup.blocksRaycasts = true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            _fadeCanvasGroup.alpha = timer / _fadeDuration;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 0;
        _fadeCanvasGroup.blocksRaycasts = false;

        _isFading = false;
    }
}
