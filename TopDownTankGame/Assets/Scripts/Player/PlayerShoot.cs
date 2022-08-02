using UnityEngine;

public class PlayerShoot : CannonShoot
{
    private PlayerComponents m_playerComponents;

    private void Start()
    {
        m_playerComponents = GetComponent<PlayerComponents>();
    }

    private void Update()
    {
        if (m_playerComponents.PlayerInputs.GetShoot())
        {
            Shoot(m_playerComponents.PlayerMovement.GetTowerDirection(), Quaternion.Euler(m_playerComponents.PlayerTowerRotation.GetTowerEulerAngles()));
        }
    }
}
