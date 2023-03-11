using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [field: SerializeField] private Button _gameBtn;
    [field: SerializeField] private Button _quitBtn;

    private void Awake()
    {
        Application.targetFrameRate = 120;

        _gameBtn.onClick.AddListener(QuitGame);
        _quitBtn.onClick.AddListener(LoadDefeatAllEnemiesLevel);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void LoadDefeatAllEnemiesLevel()
    {
        SceneManager.LoadScene("DefeatAllEnemiesLevel");
    }    
}
