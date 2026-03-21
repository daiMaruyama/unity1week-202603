using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    [Header("遷移先のシーン名")]
    [SerializeField] private string targetSceneName;

    void Start()
    {
        Button btn = GetComponent<Button>();

        btn.onClick.AddListener(() => {
            if (TwoFadeManager.Instance != null)
            {
                TwoFadeManager.Instance.StartFadeOutAndLoadScene(targetSceneName);
            }
        });
    }
}
