using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StoneController : MonoBehaviour
{
    [Header("石")]
    [SerializeField] private Transform stoneTransform;
    [SerializeField] private SpriteRenderer stoneRenderer;

    [Header("腕")]
    [SerializeField] private Transform armTransform;
    [SerializeField] private Vector3 armEntryPosition = new Vector3(-8f, -5f, 0f);

    [Header("アニメーション設定")]
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float liftedScale = 1.1f;
    [SerializeField] private float reachDuration = 0.3f;
    [SerializeField] private float openDuration = 0.5f;
    [SerializeField] private float closeDuration = 0.5f;
    [SerializeField] private float transparencyDuration = 1.5f;

    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Vector3 armGrabPosition;

    private void Start()
    {
        initialPosition = stoneTransform.position;
        initialScale = stoneTransform.localScale;
        armGrabPosition = initialPosition + Vector3.down * 0.5f;
    }

    public async UniTask PlayOpenAnimation()
    {
        // 腕が出てくる
        await DOTween.To(
            () => 0f,
            t => ApplyReach(t),
            1f,
            reachDuration
        ).SetEase(Ease.OutQuad).ToUniTask();

        // 持ち上げ
        await DOTween.To(
            () => 0f,
            t => ApplyLift(t),
            1f,
            openDuration
        ).SetEase(Ease.OutQuad).ToUniTask();
    }

    public async UniTask PlayCloseAnimation()
    {
        // 下ろす
        await DOTween.To(
            () => 1f,
            t => ApplyLift(t),
            0f,
            closeDuration
        ).SetEase(Ease.InQuad).ToUniTask();

        // 腕が引っ込む
        await DOTween.To(
            () => 1f,
            t => ApplyReach(t),
            0f,
            reachDuration
        ).SetEase(Ease.InQuad).ToUniTask();
    }

    /// <summary>
    /// t=0: 画面外  t=1: 石を掴む位置
    /// </summary>
    private void ApplyReach(float t)
    {
        armTransform.position = Vector3.Lerp(armEntryPosition, armGrabPosition, t);
    }

    /// <summary>
    /// t=0: 地面  t=1: 持ち上げた状態
    /// </summary>
    private void ApplyLift(float t)
    {
        stoneTransform.position = initialPosition + Vector3.up * moveDistance * t;
        stoneTransform.localScale = initialScale * Mathf.Lerp(1f, liftedScale, t);
        armTransform.position = stoneTransform.position + Vector3.down * 0.5f;
    }

    public async UniTask MakeTransparency()
    {
        await FadeStone(0.2f, 2f).ToUniTask();
    }

    public async UniTask ResetTransparency()
    {
        await FadeStone(1f, transparencyDuration).ToUniTask();
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
