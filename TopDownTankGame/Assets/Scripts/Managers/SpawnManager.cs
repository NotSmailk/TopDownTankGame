using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [field: Header("Spawnpoints")]
    [field: SerializeField] private Transform[] m_playerSpawnpoints;
    [field: SerializeField] private Transform[] m_enemySpawnpoints;

    [field: Header("Prefabs")]
    [field: SerializeField] GameObject m_playerPrefab;
    [field: SerializeField] GameObject[] m_enemyPrefabs;

    [field: Header("Settings")]
    [field: SerializeField] private Transform m_enemiesParent;

    private void Awake()
    {
        Transform playerSpawn = m_playerSpawnpoints[Random.Range(0, m_playerSpawnpoints.Length)];

        GameObject player = Instantiate(m_playerPrefab, playerSpawn.position, playerSpawn.rotation);

        LevelManager.AddPlayer(player.GetComponent<PlayerComponents>());

        foreach (Transform spawn in m_enemySpawnpoints)
        { 
            GameObject randEnemy = m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Length)];

            GameObject enemy = Instantiate(randEnemy, spawn.position, spawn.rotation, m_enemiesParent);

            LevelManager.AddEnemy(enemy);
        }
    }
}
