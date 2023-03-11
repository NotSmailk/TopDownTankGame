using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoPanel : MonoBehaviour
{
    [field: SerializeField] private GameObject _panel;
    [field: SerializeField] private Slider _healthBar;
    [field: SerializeField] private TextMeshProUGUI _healthText;
    [field: SerializeField] private TextMeshProUGUI _nameText;

    public void ShowPanel(bool active)
    {
        _panel.SetActive(active);
    }

    public void UpdateEnemyInfo(float maxHealth, float curHealth, string name)
    {
        _panel.SetActive(curHealth > 0);
        _healthText.text = $"{curHealth}/{maxHealth}";
        _healthBar.value = curHealth / maxHealth;
        _nameText.text = name;
    }
}
