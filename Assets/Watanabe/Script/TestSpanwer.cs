using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestSpanwer : MonoBehaviour
{
    public InsectSpawn insectSpawn;
    public Stone stone;
    public Image questionImage;
    public TMP_Text questionText;
    public TMP_Text countText;

    public void OnClickSpawn()
    {
        insectSpawn.SpawnInsects(stone.min, stone.max, stone.randomTypeCount);
    }

    public void OnClickClear()
    {
        insectSpawn.Clear();
    }

    public void OnClickQuestion()
    {
        insectSpawn.PickQuestion();

        var insect = insectSpawn.GetQuestionInsect();
        var count = insectSpawn.GetCorrectCount();

        Debug.Log($"問題：{insect.insectName}");
        Debug.Log($"正解数：{count}");

        questionText.text = insect.insectName;
        questionImage.sprite = insect.icon;
        countText.text = count.ToString();
    }
}
