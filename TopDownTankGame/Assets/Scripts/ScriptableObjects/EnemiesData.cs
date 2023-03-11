using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemies Data", fileName = "New Enemies Data")]
public class EnemiesData : ScriptableObject
{
    [field: SerializeField] private EnemyData[] _enemiesData;

    public EnemyData[] Datas => _enemiesData;
    public EnemyData RandomData => _enemiesData[Random.Range(0, _enemiesData.Length)];
}

[System.Serializable]
public class EnemyData
{
    public string Name;
    public Enemy Prefab;
    public float Health;
    public DetectionParametres DetectionParametres;
    public CannonParametres CannonParametres;
}