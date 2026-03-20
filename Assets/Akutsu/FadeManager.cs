using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] float _fadeDuration = 1.0f;

    private void Awake()
    {
        // シングルトン設定：これ一つだけを生き残らせる
        if (Instance == null)
        {
            Instance = this;
            // パネルの親（Canvasなど）ごと壊れないようにする
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // シーン読み込み完了イベントにメソッドを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // オブジェクトが消えるときは登録を解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンが切り替わるたびに自動で呼ばれる
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 前のフェードが動いていたら止めて、新しくフェードインを開始
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void StartFadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLeave(sceneName));
    }

    private IEnumerator FadeAndLeave(string sceneName)
    {
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
    }
}
