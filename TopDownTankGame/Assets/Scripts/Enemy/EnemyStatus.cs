using UnityEngine;

public class EnemyStatus : Status
{
    private EnemyComponents m_enemyComponents;

    protected override void Start()
    {
        base.Start();

        m_enemyComponents = GetComponent<EnemyComponents>();
    }

    public override void GetDamage(float damage, GameObject gunner)
    {
        base.GetDamage(damage, gunner);

        string name = $"Type: {m_tankType}";

        if (gunner.TryGetComponent(out PlayerComponents player))
            player.PlayerGameInfoUI.UpdateEnemyInfo(m_maxHp, m_curHp, name);

        if (m_curHp <= 0)
            m_enemyComponents.DestroyTank();
    }
}
