using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [field: SerializeField] private Transform[] _spawnpoints;

    public Transform RandomSpawnpoint => _spawnpoints[Random.Range(0, _spawnpoints.Length)];
    public Transform[] Spawnpoints => _spawnpoints;
}
