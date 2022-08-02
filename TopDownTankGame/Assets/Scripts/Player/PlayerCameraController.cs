using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [field: SerializeField] private Camera m_Camera;
    [field: SerializeField] private float m_heightBetweenCameraAndPlayer = 5f;

    private void Start()
    {
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        if (m_Camera == null)
            m_Camera = GetComponentInChildren<Camera>();

        AdjustCameraPosition();

        AdjustCameraRotation();
    }

    private void AdjustCameraPosition()
    {
        Vector3 newCameraPosition = new Vector3(GetPlayerPosition().x, m_heightBetweenCameraAndPlayer, GetPlayerPosition().z);

        m_Camera.transform.position = newCameraPosition;
    }

    private void AdjustCameraRotation()
    {
        m_Camera.transform.LookAt(gameObject.transform);
    }

    private Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public Camera GetCamera()
    {
        return m_Camera;
    }
}
