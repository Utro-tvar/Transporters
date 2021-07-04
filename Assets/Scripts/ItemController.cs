using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static event Action<ItemController> ItemDowned;
    public static event Action<ItemController> EndLife;
    public static event Action ItemLost;
    public static event Action ItemSaved;
    public static event Action<Color> StartFirework;

    private Color _selfColor;
    private Color _lineColor;
    private bool _enabled = false;

    public void Init(Color selfColor, Color lineColor)
    {
        _enabled = true;
        _selfColor = selfColor;
        _lineColor = lineColor;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (!_enabled) return;
        Vector3 position = GameInfo.Instanse.MainCamera.ScreenToWorldPoint(pointerEventData.position);
        if (Mathf.Abs(position.x) < 2.7f)
        {
            transform.position = new Vector3(position.x, transform.position.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (!_enabled) return;
        ItemDowned?.Invoke(this);
    } 

    public void SetLineColor(Color color)
    {
        _lineColor = color;
    }

    private void Update()
    {
        if (!_enabled) return;
        transform.position -= new Vector3(0, GameInfo.Instanse.Speed * Time.deltaTime, 0);

        if(transform.position.y < GameInfo.Instanse.DownEdge)
        {
            EndLife?.Invoke(this);
            if(_lineColor == _selfColor)
            {
                ItemSaved?.Invoke();
                StartFirework?.Invoke(_lineColor);
            }
            else
            {
                ItemLost?.Invoke();
            }
        }
    }

    private void Disable()
    {
        _enabled = false;
    }

    private void Enable()
    {
        _enabled = true;
    }

    private void OnEnable()
    {
        HP.GameOver += Disable;
        Pause.GamePaused += Disable;
        Pause.GameUnpaused += Enable;
    }

    private void OnDisable()
    {
        HP.GameOver -= Disable;
        Pause.GamePaused -= Disable;
        Pause.GameUnpaused -= Enable;
    }
}
