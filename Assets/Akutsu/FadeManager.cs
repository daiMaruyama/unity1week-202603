using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] float _fadeDuration = 0.5f;

    private void Start()
    {
        _fadeCanvasGroup.alpha = 0;
        _fadeCanvasGroup.blocksRaycasts = false;
    }

    public void ToNextScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        
        _fadeCanvasGroup.blocksRaycasts = true;// 操作不能にする

        float timer = 0;
        while (timer < _fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            _fadeCanvasGroup.alpha = timer / _fadeDuration;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 1;

        SceneManager.LoadScene(sceneName);
    }
}
