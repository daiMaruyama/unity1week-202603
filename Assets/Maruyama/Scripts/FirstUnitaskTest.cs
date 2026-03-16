using UnityEngine;
using Cysharp.Threading.Tasks;

public class FirstUnitaskTest : MonoBehaviour
{
    async UniTaskVoid Start()
    {
        Debug.Log("Hello, UniTask!");
        await UniTask.Delay(1000);
        Debug.Log("Goodbye, UniTask!");
    }
}