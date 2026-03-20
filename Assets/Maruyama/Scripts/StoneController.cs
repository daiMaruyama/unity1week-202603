using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StoneController : MonoBehaviour
{
    [SerializeField] private Transform stoneTransform;
    [SerializeField] private SpriteRenderer stoneRenderer;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float openDuration = 0.5f;
    [SerializeField] private float closeDuration = 0.5f;
    [SerializeField] private float transparencyDuration = 1.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = stoneTransform.position;
    }

    public async UniTask PlayOpenAnimation()
    {
        await stoneTransform
            .DOMoveY(initialPosition.y + moveDistance, openDuration)
            .SetEase(Ease.OutQuad)
            .ToUniTask();
    }

    public async UniTask PlayCloseAnimation()
    {
        await stoneTransform
            .DOMove(initialPosition, closeDuration)
            .SetEase(Ease.InQuad)
            .ToUniTask();
    }

    public async UniTask MakeTransparency()
    {
        await FadeStone(0.2f, 2f);
    }

    public async UniTask ResetTransparency()
    {
        await FadeStone(1f, transparencyDuration);
    }

    private Tween FadeStone(float targetAlpha, float duration)
    {
        return DOTween.To(
            () => stoneRenderer.color.a,
            a => {
                var c = stoneRenderer.color;
                c.a = a;
                stoneRenderer.color = c;
            },
            targetAlpha,
            duration
        );
    }
}