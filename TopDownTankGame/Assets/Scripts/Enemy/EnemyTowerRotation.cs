using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerRotation : TowerRotation
{
    private EnemyComponents m_enemyComponents;

    private void Start()
    {
        m_enemyComponents = GetComponent<EnemyComponents>();
    }

    private void Update()
    {
        if (m_enemyComponents.EnemyDetection.Target != null)
        {
            RotateTower(m_enemyComponents.EnemyDetection.GetTargetPosition());

            #region Debug

            Debug.DrawLine(m_tower.transform.position, m_tower.transform.position + m_tower.transform.right * 5, Color.green);

            #endregion Debug
        }
    }
}
