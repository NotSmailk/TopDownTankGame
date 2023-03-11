using UnityEngine;

[CreateAssetMenu(menuName = "Data/Players Data", fileName = "New Players Data")]
public class PlayersData : ScriptableObject
{
    [field: SerializeField] private PlayerData[] _playersData;

    public PlayerData[] Datas => _playersData;
    public PlayerData RandomData => _playersData[Random.Range(0, _playersData.Length)];
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public Player Prefab;
    public float Health;
    public CannonParametres Parametres;
}
