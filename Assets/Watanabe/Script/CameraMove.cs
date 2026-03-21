using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float slideDistance = 10f;
    [SerializeField] private float duration = 1.5f;

    private Vector3 startCameraPos;

    private void Start()
    {
        startCameraPos = cameraTransform.position; // 最初の位置を記録
    }

    public async UniTask MoveNextArea()
    {
        // 右にスライド（石が見えなくなる）
        await cameraTransform
            .DOMoveX(cameraTransform.position.x + slideDistance, duration)
            .SetEase(Ease.InOutSine)
            .ToUniTask();

        // 左の見えない位置にワープ
        cameraTransform.position = startCameraPos - new Vector3(slideDistance, 0f, 0f);

        // 元の位置までスライド
        await cameraTransform
            .DOMoveX(startCameraPos.x, duration)
            .SetEase(Ease.InOutSine)
            .ToUniTask();
    }
}
