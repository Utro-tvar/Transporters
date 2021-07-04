using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarterGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame(GameInfo gamePattern)
    {
        gamePattern.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
