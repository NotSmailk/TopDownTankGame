using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [field: SerializeField] private InGameUI _inGameUI;
    [field: SerializeField] private Task _levelTask;
    [field: SerializeField] private SpawnZone _playerZone;
    [field: SerializeField] private SpawnZone _enemyZone;
    [field: SerializeField] private EnemyFactory _enemyFactory;
    [field: SerializeField] private PlayerFactory _playerFactory;
    [field: SerializeField] private EnemiesData _enemiesData;
    [field: SerializeField] private PlayersData _playersData;

    private List<Enemy> _enemies = new List<Enemy>();
    private Player _player;
    private Task.TaskState _taskState;
    private bool _isRunning = true;

    public static List<Enemy> Enemies => _instance._enemies;
    public static TaskPanel TaskPanel => _instance._inGameUI.TaskPanel;

    private static Game _instance;

    private void Awake()
    {
        _instance = this;
        _taskState = _levelTask.InitializeTask();
        _inGameUI.Initialize();

        SpawnPlayer(_playersData.RandomData, _playerZone.RandomSpawnpoint);
    }

    private void Update()
    {
        if (_isRunning)
        {
            if (_player != null)
                _player.GameUpdate();

            _taskState.GameUpdate();
        }
    }

    public static void SpawnEnemy(EnemyData data, Transform spawnpoint)
    {
        var enemy = _instance._enemyFactory.Get(data);
        enemy.transform.position = spawnpoint.position;
        enemy.transform.rotation = spawnpoint.rotation;
        _instance._enemies.Add(enemy);
    }

    public static void SpawnEnemy(EnemyData data, int spawnpointIndex)
    {
        if (_instance._enemyZone.Spawnpoints.Length < spawnpointIndex)
            return;

        var enemy = _instance._enemyFactory.Get(data);
        enemy.transform.position = _instance._enemyZone.Spawnpoints[spawnpointIndex].position;
        enemy.transform.rotation = _instance._enemyZone.Spawnpoints[spawnpointIndex].rotation;
        _instance._enemies.Add(enemy);
    }

    public static void SpawnPlayer(PlayerData data, Transform spawnpoint)
    {
        var player = _instance._playerFactory.Get(data);
        player.transform.position = spawnpoint.position;
        player.transform.rotation = spawnpoint.rotation;
        _instance._player = player;
        _instance._inGameUI.ShowTaskPanel(true);
    }

    public static void UpdatePlayerInfo(Player player)
    {
        _instance._inGameUI.UpdatePlayerInfo(player);
    }

    public static void UpdateEnemyInfo(Enemy enemy)
    {
        _instance._inGameUI.UpdateEnemyInfo(enemy);
    }

    public static void Victory()
    {
        _instance._inGameUI.ShowWinPanel();
        _instance._isRunning = false;
    }

    public static void Defeat()
    {
        _instance._inGameUI.ShowLosePanel();
        _instance._isRunning = false;
    }

    public static void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
