using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InsectSpawn : MonoBehaviour
{
    [Header("虫の設定")]
    public InsectData pillBug;
    public List<InsectData> otherInsects;
    [Header("生成位置")]
    public Transform spawnArea;
    //生成済みの虫リスト
    private List<GameObject> spawnInsects = new List<GameObject>();
    //問題用の虫と正解数
    private InsectData questionInsect;
    private int correctCount;

    
    /// <summary>
    /// 虫のスポーン
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="randomTypeCount"></param>
    public void SpawnInsects(int min,int max,int randomTypeCount)
    {
        //出現する数を決める
        int totalCount = Random.Range(min,max + 1);
        //出現させる虫のリスト
        List<InsectData> allTypes = new List<InsectData>() {pillBug };
        int typesToSelect = Mathf.Min(randomTypeCount, otherInsects.Count);
        if (typesToSelect > 0)
        {
            while(allTypes.Count -1 < typesToSelect) //ダンゴムシを除くため-1
            {
                InsectData data = otherInsects[Random.Range(0, otherInsects.Count)];
                if(!allTypes.Contains(data))
                    allTypes.Add(data);
            }
        }

        //種類ごとに合計数を分配して生成
        int remaining = totalCount;
        for(int i = 0; i < allTypes.Count; i++)
        {
            int count = (i == allTypes.Count - 1) ? remaining : Random.Range(0, remaining + 1);
            remaining -= count;
            for(int j = 0; j < count; j++)
            {
                GameObject obj = Instantiate(allTypes[i].prefab,spawnArea);
                spawnInsects.Add(obj);
                //Insect コンポーネントにデータ紐付け
                Insect insectComponent = obj.GetComponent<Insect>();
                if (insectComponent != null)
                    insectComponent.data = allTypes[i];
            }
        }
    }

    /// <summary>
    /// 出題する虫を決める
    /// </summary>
    public void PickQuestion()
    {
        if(spawnInsects.Count == 0)
        {
            questionInsect = pillBug;
            correctCount = 0;
            return;
        }

        int idx = Random.Range(0,spawnInsects.Count);
        questionInsect = spawnInsects[idx].GetComponent<Insect>()?.data;
        correctCount = CountInsect(questionInsect);
    }

    /// <summary>
    /// 指定した虫の数をカウント
    /// </summary>
    public int CountInsect(InsectData target)
    {
        int count = 0;
        foreach (var obj in spawnInsects)
        {
            Insect insect = obj.GetComponent<Insect>();
            if (insect != null && insect.data == target)
                count++;
        }
        return count;
    }

    /// <summary>
    /// ラウンド終了時に虫を消す
    /// </summary>
    public void Clear()
    {
        spawnInsects.ForEach(i => Destroy(i));
        spawnInsects.Clear();
        questionInsect = null;
        correctCount = 0;
    }

    /// <summary>
    /// 出題虫を取得
    /// </summary>
    public InsectData GetQuestionInsect()
    {
        return questionInsect;
    }

    /// <summary>
    /// 正解数を取得
    /// </summary>
    public int GetCorrectCount()
    {
        return correctCount;
    }

}
