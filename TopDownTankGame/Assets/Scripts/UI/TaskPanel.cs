using TMPro;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    [field: SerializeField] private GameObject _taskPanel;
    [field: SerializeField] private TextMeshProUGUI _taskInfo;
    [field: SerializeField] private TextMeshProUGUI _taskGoal;

    public void ShowPanel(bool active)
    {
        _taskPanel.SetActive(active);
    }

    public void UpdateTaskInfo(string taskInfo, string taskGoal)
    {
        if (_taskPanel == null)
            return;

        _taskInfo.text = taskInfo;
        _taskGoal.text = taskGoal;
    }
}
