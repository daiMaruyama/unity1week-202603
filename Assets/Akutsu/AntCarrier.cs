using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AntCarrier : MonoBehaviour
{
    [SerializeField] Slider _slider;        // SEスライダー
    [SerializeField] GameObject _sugar;     // 砂糖のオブジェクト
    [SerializeField] GameObject _antObjParent; // アリの居場所を子供にしたオブジェクト
    List<GameObject> _antSpawn = new List<GameObject>(); // アリが生まれる場所
    bool _hasFood = false; // エサを持っているか
    int _sugarHealth = 1; // 砂糖の残り具合
    int _maxHealth = 1;
    float _baseScale; // 画像の倍率に合わせる
    float _volumeMax;
    float _volumeMin;

    private void Awake()
    {
        foreach (Transform ant in _antObjParent.transform)// 表示されているアリの画像を非表示にしておく
        {
            ant.gameObject.SetActive(false);
            _antSpawn.Add(ant.gameObject);
        }
    }

    void Start()
    {
        _baseScale = _sugar.transform.localScale.x;
        _maxHealth = _antSpawn.Count;
        _sugarHealth = _maxHealth;
        _slider.onValueChanged.AddListener(OnSliderChanged);　// スライダーの値が変更された時にメソッドを呼ぶ設定
        _volumeMax = _slider.maxValue - 0.05f;
        _volumeMin = _slider.minValue + 0.05f;
    }

    void OnSliderChanged(float value)
    {
        if (value >= _volumeMax && !_hasFood && _sugarHealth > 0)// 右端に着いた
        {
            _hasFood = true;
            _sugarHealth--;

            float ratio = (float)_sugarHealth / _maxHealth;
            float nextScale = Mathf.Max(0, ratio * _baseScale);
            _sugar.transform.localScale = new Vector3(nextScale, nextScale, nextScale);

            Debug.Log("エサをゲット！");
        }

        if (value <= _volumeMin && _hasFood && _antSpawn.Count > 0)// 左端に戻ってきた
        {
            _hasFood = false;
            _antSpawn[0].SetActive(true);
            _antSpawn.RemoveAt(0);
            Debug.Log("エサを運んだ！仲間が増えた！");
        }
    }
}
