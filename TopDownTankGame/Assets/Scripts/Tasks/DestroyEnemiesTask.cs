using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/Destroy Enemies", fileName = "New Destroy Enemies Task")]
public class DestroyEnemiesTask : Task
{
    [field: SerializeField] private int _enemiesCount;
    [field: SerializeField] private EnemiesData _enemies;

    public override TaskState InitializeTask()
    {
        var state = new DestroyEnemiesTaskState(this);
        return state;
    }

    public class DestroyEnemiesTaskState : TaskState
    {
        private List<Enemy> _enemies = new List<Enemy>();
        private int _count;
        private string _taskInfo;

        public DestroyEnemiesTaskState(DestroyEnemiesTask task)
        {
            _count = task._enemiesCount;
            _taskInfo = task.TaskInfo;

            for (int i = 0; i < _count; i++)
            {
                Game.SpawnEnemy(task._enemies.RandomData, i);
            }

            _enemies = Game.Enemies;

            OnTaskCompleted.AddListener(Complete);
            OnTaskFulfiling.AddListener(Fullfilment);

            Fullfilment();
        }

        public override void GameUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].GameUpdate())
                {
                    int lastIndex = _enemies.Count - 1;
                    _enemies[i] = _enemies[lastIndex];
                    _enemies.RemoveAt(lastIndex);
                    i--;
                    OnTaskFulfiling.Invoke();

                    if (_enemies.Count <= 0)
                        OnTaskCompleted.Invoke();
                }
            }
        }

        public void Fullfilment()
        {
            string taskGoal = $"Enemies defeated: {_enemies.Count}/{_count}";
            Game.TaskPanel.UpdateTaskInfo(_taskInfo, taskGoal);
        }

        public void Complete()
        {
            string taskGoal = $"Cleared";
            Game.TaskPanel.UpdateTaskInfo(_taskInfo, taskGoal);
            Game.Victory();
        }
    }
}
