using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static event Action GameOver;


    private IHPIndicator _indicator;

    private void Start()
    {
        _indicator = GetComponent<IHPIndicator>();
        _indicator.Init(GameInfo.Instanse.HP);
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
        if (!_indicator.TryRemoveHP() || _indicator.GetHP() == 0) GameOver?.Invoke();
    }
}
