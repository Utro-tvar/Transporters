using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI LosePanelScore;

    private TextMeshProUGUI _text;
    private int _score;

    private void Start()
    {
        _score = 0;
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        HP.GameOver += SetFinalScore;
        ItemController.ItemSaved += AddScore;
    }

    private void OnDisable()
    {
        HP.GameOver -= SetFinalScore;
        ItemController.ItemSaved -= AddScore;
    }

    private void AddScore()
    {
        ++_score;
        _text.text = "Score: " + _score;
    }

    private void SetFinalScore()
    {
        LosePanelScore.text = "Score: " + _score;
    }
}
