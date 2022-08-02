using UnityEngine;
using System.Threading.Tasks;

public class CannonShoot : MonoBehaviour
{
    [field: SerializeField] protected Transform m_shootPoint;
    [field: SerializeField] protected Shell m_shell;
    [field: SerializeField] protected int m_cooldown;
    [field: SerializeField] protected int m_damage;

    protected bool m_isReloading = false;

    public void Shoot(Vector3 direction, Quaternion rotation)
    {
        if (m_isReloading)
            return;

        Shell shell = Instantiate(m_shell, m_shootPoint.position, rotation);

        shell.Launch(direction, gameObject, m_damage);

        Reload(m_cooldown);
    }

    public async void Reload(int cooldown)
    {
        m_isReloading = true;

        cooldown *= 1000;

        await Task.Delay(cooldown);

        m_isReloading = false;
    }
}
