using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [field: SerializeField] private LevelTask m_levelTask;

    public static List<GameObject> Enemies = new List<GameObject>();
    public static GameObject Player;
    public static PlayerGameInfoUI PlayerGameInfoUI;

    private void Start()
    {
        if (m_levelTask == null)
            return;

        LevelTask task = Instantiate(m_levelTask, transform);

        task.InitializeTask();
    }

    private void OnDisable()
    {
        Enemies.Clear();
    }

    public static void AddEnemy(GameObject enemy)
    {
        if (Enemies.Contains(enemy))
            return;

        Enemies.Add(enemy);
    }

    public static void AddPlayer(PlayerComponents player)
    {
        if (Player == player)
            return;

        Player = player.gameObject;

        PlayerGameInfoUI = player.PlayerGameInfoUI;

        PlayerGameInfoUI.ShowTaskPanel(true);
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
