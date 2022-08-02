using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : CannonShoot
{
    private EnemyComponents m_enemyComponents;

    private void Start()
    {
        m_enemyComponents = GetComponent<EnemyComponents>();
    }

    private void Update()
    {
        if (m_enemyComponents.EnemyDetection.TargetInShootDistance())
        {
            Shoot(m_enemyComponents.EnemyMovement.GetTowerDirection(), Quaternion.Euler(m_enemyComponents.EnemyTowerRotation.GetTowerEulerAngles()));
        }
    }
}
