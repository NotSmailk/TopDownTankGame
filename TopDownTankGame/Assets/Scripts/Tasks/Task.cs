using UnityEngine;
using UnityEngine.Events;

public class Task : ScriptableObject
{
    [field: SerializeField] public string TaskInfo;

    public virtual TaskState InitializeTask() => new TaskState();

    public class TaskState 
    {
        public UnityEvent OnTaskCompleted { get; } = new UnityEvent();
        public UnityEvent OnTaskFulfiling { get; } = new UnityEvent();

        public virtual void GameUpdate()
        {

        }
    }
}
