using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerGameInfoUI : MonoBehaviour
{
    [field: Header("Player Components")]
    [field: SerializeField] Slider m_playerHpBar;
    [field: SerializeField] TextMeshProUGUI m_playerHpText;

    [field: Header("Enemy Components")]
    [field: SerializeField] GameObject m_enemyInfoPanel;
    [field: SerializeField] Slider m_enemyHpBar;
    [field: SerializeField] TextMeshProUGUI m_enemyHpText;
    [field: SerializeField] TextMeshProUGUI m_enemyName;

    [field: Header("Task Components")]
    [field: SerializeField] GameObject m_taskPanel;
    [field: SerializeField] TextMeshProUGUI m_taskInfo;
    [field: SerializeField] TextMeshProUGUI m_taskGoal;

    [field: Header("Win/Lose Components")]
    [field: SerializeField] GameObject m_winPanel;
    [field: SerializeField] Button m_winPanelRetry;
    [field: SerializeField] Button m_winPanelQuit;
    [field: SerializeField] GameObject m_losePanel;
    [field: SerializeField] Button m_losePanelRetry;
    [field: SerializeField] Button m_losePanelQuit;

    [field: HideInInspector] public UnityEvent<string, string> OnTaskFulfilment;
    [field: HideInInspector] public UnityEvent OnTaskComplete;

    private void Awake()
    {
        m_enemyInfoPanel.SetActive(false);
        m_taskPanel.SetActive(false);
        m_losePanel.SetActive(false);
        m_winPanel.SetActive(false);

        OnTaskFulfilment.AddListener(UpdateTaskInfo);
        OnTaskComplete.AddListener(PlayerWin);
    }

    private void OnEnable()
    {
        m_winPanelRetry?.onClick.AddListener(LevelManager.Retry);
        m_losePanelRetry?.onClick.AddListener(LevelManager.Retry);

        m_winPanelQuit?.onClick.AddListener(LevelManager.MainMenu);
        m_losePanelQuit?.onClick.AddListener(LevelManager.MainMenu);

    }

    public void UpdatePlayerHpBar(float hpMax, float hpCur)
    {
        m_playerHpBar.value = hpCur / hpMax;

        m_playerHpText.text = $"{hpCur}/{hpMax}";
    }

    public void UpdateEnemyInfo(float hpMax, float hpCur, string name)
    {
        m_enemyInfoPanel.SetActive(hpCur > 0);

        m_enemyHpText.text = $"{hpCur}/{hpMax}";

        m_enemyHpBar.value = hpCur / hpMax;

        m_enemyName.text = name;
    }

    public void ShowTaskPanel(bool active)
    {
        m_taskPanel.SetActive(active);
    }

    public void UpdateTaskInfo(string taskInfo, string taskGoal)
    {
        if (m_taskPanel == null)
            return;

        m_taskInfo.text = taskInfo;

        m_taskGoal.text = taskGoal;
    }

    public void PlayerDefeat()
    {
        m_losePanel.SetActive(true);
    }

    public void PlayerWin()
    {
        GetComponent<PlayerComponents>().DiableTank();

        m_winPanel.SetActive(true);
    }
}
