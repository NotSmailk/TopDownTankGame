using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [field: SerializeField] private KeyCode m_forwardButton     = KeyCode.W;
    [field: SerializeField] private KeyCode m_backwardButton    = KeyCode.S;
    [field: SerializeField] private KeyCode m_leftButton        = KeyCode.A;
    [field: SerializeField] private KeyCode m_rightButton       = KeyCode.D;
    [field: SerializeField] private KeyCode m_shootButton       = KeyCode.Mouse0;

    public bool GetShoot()
    {
        return GetKeyDown(m_shootButton);
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public int GetVerticalAxis()
    {
        if (GetKey(m_forwardButton))
            return 1;

        if (GetKey(m_backwardButton))
            return -1;

        return 0;
    }

    public int GetHorizontalAxis()
    {
        if (GetKey(m_rightButton))
            return 1;

        if (GetKey(m_leftButton))
            return -1;

        return 0;
    }

    private bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    private bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    private bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
}
