using UnityEngine;

public class EnemyTowerRotation : TowerRotation
{
    private EnemyDetection _detection;

    public void Initialize(EnemyDetection detection, GameObject tower)
    {
        _detection = detection;
        _tower = tower;
    }

    public void GameUpdate()
    {
        if (_detection.Target != null)
        {
            RotateTower(_detection.GetTargetPosition());

            #if UNITY_EDITOR
            Debug.DrawLine(_tower.transform.position, _tower.transform.position + _tower.transform.right * 5, Color.green);
            #endif
        }
    }
}
