using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StoneController : MonoBehaviour
{
    [SerializeField] private Transform stoneTransform;
    [SerializeField] private Ease openEase = Ease.OutBack;
    [SerializeField] private Ease closeEase = Ease.InBack;
    [SerializeField] private Ease fadeEase = Ease.Linear;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float animDuration = 1.5f;

    public async UniTask PlayOpenAnimation()
    {
        await stoneTransform
            .DOMoveY(stoneTransform.position.y + moveDistance, animDuration)
            .SetEase(openEase)
            .ToUniTask();
    }

    public async UniTask PlayCloseAnimation()
    {
        await stoneTransform
            .DOMoveY(stoneTransform.position.y - moveDistance, animDuration)
            .SetEase(closeEase)
            .ToUniTask();
    }

    public async UniTask MakeTransparency()
    {
        var material = stoneTransform.GetComponent<Renderer>().material;
        await material
            .DOFade(0.5f, animDuration)
            .SetEase(fadeEase)
            .ToUniTask();
    }

    public async UniTask ResetTransparency()
    {
        var material = stoneTransform.GetComponent<Renderer>().material;
        await material
            .DOFade(1f, animDuration)
            .SetEase(fadeEase)
            .ToUniTask();
    }
}