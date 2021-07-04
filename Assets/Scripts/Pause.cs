using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static event Action GamePaused;
    public static event Action GameUnpaused;

    public GameObject PausePanel;

    public void PauseGame()
    {
        Time.timeScale = 0;
        GamePaused?.Invoke();
        PausePanel.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        GameUnpaused?.Invoke();
        PausePanel.SetActive(false);
    }
}
