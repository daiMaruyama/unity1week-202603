using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private TextMeshProUGUI tensDigitText;
    [SerializeField] private TextMeshProUGUI onesDigitText;
    [SerializeField] private Button tensUpButton;
    [SerializeField] private Button tensDownButton;
    [SerializeField] private Button onesUpButton;
    [SerializeField] private Button onesDownButton;
    [SerializeField] private Button submitButton;
    #endregion

    private int tens;
    private int ones;
    private UniTaskCompletionSource<int> tcs;

    // リスナー登録は1回だけ
    private void Start()
    {
        tensUpButton.onClick.AddListener(() => ChangeTens(1));
        tensDownButton.onClick.AddListener(() => ChangeTens(-1));
        onesUpButton.onClick.AddListener(() => ChangeOnes(1));
        onesDownButton.onClick.AddListener(() => ChangeOnes(-1));
        submitButton.onClick.AddListener(OnSubmit);
    }

    // GMがawaitするのはこれだけ
    public UniTask<int> WaitForInput()
    {
        tens = 0;
        ones = 0;
        UpdateDisplay();
        tcs = new UniTaskCompletionSource<int>();
        return tcs.Task;
    }

    private void ChangeTens(int dir)
    {
        tens = (tens + dir + 10) % 10;
        UpdateDisplay();
        SEManager.Instance.PlaySelect();
    }

    private void ChangeOnes(int dir)
    {
        ones = (ones + dir + 10) % 10;
        UpdateDisplay();
        SEManager.Instance.PlaySelect();
    }

    private void UpdateDisplay()
    {
        tensDigitText.text = tens.ToString();
        onesDigitText.text = ones.ToString();
    }

    private void OnSubmit()
    {
        tcs?.TrySetResult(tens * 10 + ones);
    }
}
