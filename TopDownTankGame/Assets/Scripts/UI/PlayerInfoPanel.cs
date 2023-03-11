using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour
{
    [field: SerializeField] private Slider _healthBar;
    [field: SerializeField] private TextMeshProUGUI _healthText;

    public void UpdatePlayerHelathBar(float maxHealth, float curHealth)
    {
        _healthBar.value = curHealth / maxHealth;
        _healthText.text = $"{curHealth}/{maxHealth}";
    }
}
