using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;

    private void OnEnable()
    {
        HP.GameOver += GameOver;
    }

    private void OnDisable()
    {
        HP.GameOver -= GameOver;
    }

    public void ToMainMenu()
    {
        GameInfo.Instanse.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _losePanel.SetActive(true);
    }
}
