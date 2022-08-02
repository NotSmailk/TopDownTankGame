using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadDefeatAllEnemiesLevel()
    {
        SceneManager.LoadScene("DefeatAllEnemiesLevel");
    }    
}
