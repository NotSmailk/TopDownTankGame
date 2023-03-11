using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [field: SerializeField] private PlayerInfoPanel _playerInfoPanel;
    [field: SerializeField] private EnemyInfoPanel _enemyInfoPanel;
    [field: SerializeField] private TaskPanel _taskPanel;
    [field: SerializeField] private PopUpPanel _winPanel;
    [field: SerializeField] private PopUpPanel _losePanel;

    public TaskPanel TaskPanel => _taskPanel;

    public void Initialize()
    {
        _enemyInfoPanel.ShowPanel(false);
        _winPanel.ShowPanel(false);
        _losePanel.ShowPanel(false);

        _winPanel.LeftButton?.onClick.AddListener(Game.Retry);
        _winPanel.RightButton?.onClick.AddListener(Game.MainMenu);
        _losePanel.LeftButton?.onClick.AddListener(Game.Retry);
        _losePanel.RightButton?.onClick.AddListener(Game.MainMenu);
    }

    public void ShowLosePanel()
    {
        _losePanel.ShowPanel(true);
    }

    public void ShowWinPanel()
    {
        _winPanel.ShowPanel(true);
    }

    public void ShowTaskPanel(bool active)
    {
        _taskPanel.ShowPanel(active);
    }

    public void UpdateTaskInfo(string taskInfo, string taskGoal)
    {
        _taskPanel.UpdateTaskInfo(taskInfo, taskGoal);
    }

    public void UpdatePlayerInfo(Player player)
    {
        _playerInfoPanel.UpdatePlayerHelathBar(player.HealthInfo.Item1, player.HealthInfo.Item2);
    }

    public void UpdateEnemyInfo(Enemy enemy)
    {
        _enemyInfoPanel.UpdateEnemyInfo(enemy.EnemyInfo.Item1, enemy.EnemyInfo.Item2, enemy.EnemyInfo.Item3);
    }
}
