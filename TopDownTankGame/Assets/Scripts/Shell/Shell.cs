using System.Threading.Tasks;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [field: SerializeField] private int m_speed = 25;

    private bool m_shellAcitvity = false;
    private bool m_hitted = false;
    private int m_damage = 25;
    private Vector3 m_startPosition;
    private GameObject m_gunner;

    public GameObject Gunner { get => m_gunner; }
    public Vector3 StartPosition { get => m_startPosition; }

    public void Launch(Vector3 direction, GameObject gunner, int damage = 25)
    {
        GetComponent<Rigidbody>().velocity = direction * m_speed;

        m_startPosition = transform.position;
        m_damage = damage;
        m_gunner = gunner;

        LaunchCoolDown(25);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!m_shellAcitvity)
            return;

        Destroy(gameObject);

        HitTank(collision.gameObject);

        DestroyEnvironment(collision.gameObject);
    }

    private void HitTank(GameObject hitObject)
    {
        Status status = hitObject.TryGetComponent(out status) ? status : hitObject.GetComponent<LinkToStatus>()?.Status;

        if (status == null || status.gameObject == m_gunner)
            return;

        status.OnGetDamage.Invoke(m_damage, m_gunner);
    }

    private void DestroyEnvironment(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out DestroyableEnvironment hit))
            hit.DestroyObject();
    }

    public async void LaunchCoolDown(int cooldown)
    {
        await Task.Delay(cooldown);

        m_shellAcitvity = true;
    }
}
