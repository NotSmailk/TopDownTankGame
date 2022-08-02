using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemies : LevelTask
{
    private List<GameObject> m_enemies = new List<GameObject>();

    private int m_defeatedEnemyCount = 0;

    private void Start()
    {
        OnTaskFulfiling.AddListener(UpdateTaskInfo);

        OnTaskCompleted.AddListener(TaskComplete);
    }

    public override void InitializeTask()
    {
        m_enemies = LevelManager.Enemies;

        foreach (GameObject enemy in m_enemies)
            enemy.AddComponent<DefeatAllEnemiesTaskObject>().LevelTask = this;

        m_taskGoal = $"Enemies defeated {m_defeatedEnemyCount}/{m_enemies.Count}";

        if (LevelManager.Player != null)
            LevelManager.PlayerGameInfoUI.OnTaskFulfilment.Invoke(m_taskInfo, m_taskGoal);
    }

    public override void UpdateTaskInfo()
    {
        m_defeatedEnemyCount++;

        m_taskGoal = $"Enemies defeated {m_defeatedEnemyCount}/{m_enemies.Count}";

        if (LevelManager.Player != null)
            LevelManager.PlayerGameInfoUI.OnTaskFulfilment.Invoke(m_taskInfo, m_taskGoal);

        if (m_defeatedEnemyCount.Equals(m_enemies.Count))
            OnTaskCompleted.Invoke();
    }

    public override void TaskComplete()
    {
        m_taskGoal = "Cleared";

        if (LevelManager.Player != null)
        {
            LevelManager.PlayerGameInfoUI.OnTaskFulfilment.Invoke(m_taskInfo, m_taskGoal);

            LevelManager.PlayerGameInfoUI.OnTaskComplete.Invoke();
        }
    }
}