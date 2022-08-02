using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelTask : MonoBehaviour
{
    [field: HideInInspector] public UnityEvent OnTaskCompleted;
    [field: HideInInspector] public UnityEvent OnTaskFulfiling;

    [field: SerializeField] protected string m_taskInfo;

    protected string m_taskGoal;

    public abstract void InitializeTask();
    public abstract void UpdateTaskInfo();
    public abstract void TaskComplete();
}
