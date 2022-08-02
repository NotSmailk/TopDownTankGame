using UnityEngine;

public class PlayerTowerRotation : TowerRotation
{
    private PlayerComponents m_playerComponents;
    private Camera m_Camera;

    private void Start()
    {
        m_playerComponents = GetComponent<PlayerComponents>();

        m_Camera = GetComponent<PlayerCameraController>().GetCamera();
    }

    private void Update()
    {
        RotateTower(GetWorldMousePosition());

        #region Debug

        Debug.DrawLine(m_tower.transform.position, m_tower.transform.position + m_tower.transform.right * 5, Color.green);

        #endregion Debug
    }

    private Vector3 GetWorldMousePosition()
    {
        return m_Camera.ScreenToWorldPoint(m_playerComponents.PlayerInputs.GetMousePosition() + Vector3.forward * m_Camera.transform.position.y);
    }
}
