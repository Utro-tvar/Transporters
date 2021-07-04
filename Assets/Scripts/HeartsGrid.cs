using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsGrid : MonoBehaviour
{
    public enum StartPosition { Left, Right}

    [Header("Padding")]
    [SerializeField] private int _left = 0;
    [SerializeField] private int _right = 0;
    [SerializeField] private int _top = 0;
    [SerializeField] private int _bottom = 0;
    [Space(10)]
    [SerializeField] private int _spacing = 0;
    [SerializeField] private StartPosition _start = StartPosition.Left;
    [Space(10)]
    [SerializeField] private GameObject _heart;

    private Stack<GameObject> _hearts = new Stack<GameObject>();
    private Vector2 _heartsSize;
    private Vector2 _newPosition;
    private int _direction;

    public void AddHeart()
    {
        GameObject heart = Instantiate(_heart, transform);
        _hearts.Push(heart);
        heart.transform.localPosition = _newPosition;
        _newPosition += new Vector2((_heartsSize.x + _spacing) * _direction, 0);
    }

    public bool TryRemoveHeart()
    {
        if(_hearts.Count > 0)
        {
            Destroy(_hearts.Pop());
            return true;
        }
        return false;
    }

    public int GetHeartsCount()
    {
        return _hearts.Count;
    }

    private void Awake()
    {
        Vector2 size = (transform as RectTransform).rect.size;
        _heartsSize = Vector2.one * (size.y - _top - _bottom);
        switch (_start)
        {
            case StartPosition.Left:
                _newPosition = new Vector2(_left, _bottom) + Vector2.one * (_heartsSize.x / 2);
                _direction = 1;
                break;
            case StartPosition.Right:
                _newPosition = new Vector2(size.x - _right, _bottom) + new Vector2(-_heartsSize.x / 2, _heartsSize.x / 2);
                _direction = -1;
                break;
        }
        (_heart.transform as RectTransform).sizeDelta = _heartsSize;
    }
}
