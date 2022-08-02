using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemiesTaskObject : TaskObject
{
    private void OnDestroy()
    {
        LevelTask.OnTaskFulfiling.Invoke();
    }
}
