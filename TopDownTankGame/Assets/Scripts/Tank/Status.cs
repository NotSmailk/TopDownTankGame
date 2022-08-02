using UnityEngine;
using UnityEngine.Events;

public class Status : MonoBehaviour
{
    [field: SerializeField] protected float m_maxHp = 100f;
    [field: SerializeField] protected TankType m_tankType = TankType.Snake;

    [field: HideInInspector] public UnityEvent<float, GameObject> OnGetDamage;

    protected float m_curHp = 100f;

    protected virtual void Start()
    {
        m_curHp = m_maxHp;

        OnGetDamage.AddListener(GetDamage);
    }

    public virtual void GetDamage(float damage, GameObject gunner)
    {
        if (m_curHp <= 0)
            return;

        m_curHp -= damage;
    }
}

public enum TankType
{ 
    Cheetah,
    Snake,
    Pather,
    Elephant
}
