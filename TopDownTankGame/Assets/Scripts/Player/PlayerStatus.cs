using UnityEngine;

public class PlayerStatus : Status
{
    private PlayerComponents m_playerComponents;

    protected override void Start()
    {
        base.Start();

        m_playerComponents = GetComponent<PlayerComponents>();

        m_playerComponents.PlayerGameInfoUI.UpdatePlayerHpBar(m_maxHp, m_curHp);
    }

    

    public override void GetDamage(float damage, GameObject gunner)
    {
        base.GetDamage(damage, gunner);

        m_playerComponents.PlayerGameInfoUI.UpdatePlayerHpBar(m_maxHp, m_curHp);

        if (m_curHp <= 0)
            m_playerComponents.DestroyTank();
    }
}
