using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [field: SerializeField] private GameObject _panel;
    [field: SerializeField] private Button _leftButton;
    [field: SerializeField] private Button _rightButton;

    public Button LeftButton => _leftButton;
    public Button RightButton => _rightButton;

    public void ShowPanel(bool active)
    {
        _panel.SetActive(active);
    }
}
