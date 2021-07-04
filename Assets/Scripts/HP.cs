using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static event Action GameOver;


    private HeartsGrid _heartGrid;

    private void Start()
    {
        _heartGrid = GetComponent<HeartsGrid>();
        for(int i = 0; i < GameInfo.Instanse.HP; ++i)
        {
            _heartGrid.AddHeart();
        }
    }

    private void OnEnable()
    {
        ItemController.ItemLost += RemoveHeart;
    }

    private void OnDisable()
    {
        ItemController.ItemLost -= RemoveHeart;
    }

    private void RemoveHeart()
    {
        _heartGrid.TryRemoveHeart();
        if (_heartGrid.GetHeartsCount() == 0) GameOver?.Invoke();
    }
}
