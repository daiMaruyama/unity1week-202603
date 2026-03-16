using UnityEngine;

[CreateAssetMenu(fileName = "InsectData",menuName = "GameData/InsectData")]
public class InsectData : ScriptableObject
{
    public string insectName;
    public GameObject prefab;
}
