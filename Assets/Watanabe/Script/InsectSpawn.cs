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
    public Vector2 spawnRange;
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

        int typeCount = allTypes.Count;

        // totalCount が typeCount より少ない場合は調整
        if (totalCount < typeCount)
        {
            typeCount = totalCount;
            allTypes = allTypes.GetRange(0, typeCount);
        }

        // 各種類にまず1匹ずつ割り振る（totalCount が少ない場合は最小値で調整）
        int remaining = totalCount - typeCount;
        int[] counts = new int[typeCount];
        for (int i = 0; i < typeCount; i++)
            counts[i] = 1;

        // 残りをランダムに割り振る
        for (int i = 0; i < remaining; i++)
        {
            int idx = Random.Range(0, typeCount);
            counts[idx]++;
        }

        //生成時に位置をランダム化
        for (int i = 0; i < typeCount; i++)
        {
            for (int j = 0; j < counts[i]; j++)
            {
                if (!GetRandomPosition(out Vector3 pos))
                    continue;

                GameObject obj = Instantiate(allTypes[i].prefab, pos, Quaternion.identity, spawnArea);
                spawnInsects.Add(obj);

                Insect insectComponent = obj.GetComponent<Insect>();
                if (insectComponent != null)
                    insectComponent.data = allTypes[i];
            }
        }
    }

    private bool GetRandomPosition(out Vector3 pos)
    {
        for (int i = 0; i < 30; i++) 
        {
            pos = spawnArea.position + new Vector3(
                Random.Range(-spawnRange.x / 2f, spawnRange.x / 2f),
                Random.Range(-spawnRange.y / 2f, spawnRange.y / 2f),
                0f
            );

            bool tooClose = false; //他の虫と指定した距離分空いてるかどうか
            foreach (var obj in spawnInsects)
            {
                if (Vector3.Distance(obj.transform.position, pos) < 0.6f)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return true;
        }

        pos = Vector3.zero;
        return false; 
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
        Debug.Log($"{questionInsect?.insectName} の正解数: {correctCount}匹");
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

    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;

        // 黄色いワイヤーフレームで範囲を表示
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea.position, new Vector3(spawnRange.x, spawnRange.y, 0));

        // 中心点もわかるように小さな球
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnArea.position, 0.1f);
    }
}
