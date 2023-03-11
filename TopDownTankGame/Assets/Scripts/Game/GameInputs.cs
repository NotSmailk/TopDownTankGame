using UnityEngine;

public class GameInputs : MonoBehaviour
{
    [field: SerializeField] private KeyCode _forwardButton     = KeyCode.W;
    [field: SerializeField] private KeyCode _backwardButton    = KeyCode.S;
    [field: SerializeField] private KeyCode _leftButton        = KeyCode.A;
    [field: SerializeField] private KeyCode _rightButton       = KeyCode.D;
    [field: SerializeField] private KeyCode _shootButton       = KeyCode.Mouse0;

    private static GameInputs _instance;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public static bool GetShoot()
    {
        return GetKeyDown(_instance._shootButton);
    }

    public static Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public static int GetVerticalAxis()
    {
        if (GetKey(_instance._forwardButton))
            return 1;

        if (GetKey(_instance._backwardButton))
            return -1;

        return 0;
    }

    public static int GetHorizontalAxis()
    {
        if (GetKey(_instance._rightButton))
            return 1;

        if (GetKey(_instance._leftButton))
            return -1;

        return 0;
    }

    private static bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    private static bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    private static bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
}
