using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [Range(0, 3)] public int Index = 0;
    [SerializeField] private SpriteRenderer _lineCell;
    [SerializeField] private Transform _downEdge;

    private Color _lineColor;
    private float _minHeight;
    private float _maxHeight;
    private ParticleSystem _firework;

    private Queue<Transform> _cells;

    private void Start()
    {
        _firework = GetComponentInChildren<ParticleSystem>();
        _lineColor = GameInfo.Instanse.Lines[Index].Color;
        //_lineColor = GameInfo.Instanse.Colors[Index];
        foreach (SpriteRenderer sprite in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = _lineColor;
        }

        _minHeight = GameInfo.Instanse.DownEdge - 1;
        _maxHeight = GameInfo.Instanse.TopEdge + 1;
        _cells = new Queue<Transform>();
        float step = _lineCell.sprite.bounds.size.y;
        float position = _minHeight;
        while (position < _maxHeight)
        {
            _cells.Enqueue(Instantiate(_lineCell.gameObject, new Vector3(transform.position.x, position, 0), Quaternion.identity, transform).transform);
            position += step;
        }
        _maxHeight = _minHeight + (int)((_maxHeight - _minHeight) / step) * step;

        _downEdge.position = new Vector3(_downEdge.position.x, GameInfo.Instanse.DownEdge + 0.05f, _downEdge.position.z);
    }

    private void Update()
    {
        foreach (Transform cell in _cells)
        {
            cell.position += new Vector3(0, -GameInfo.Instanse.Speed * Time.deltaTime, 0);
        }
        if (_cells.Peek().position.y < _minHeight)
        {
            Transform cell = _cells.Dequeue();
            cell.position += new Vector3(0, _maxHeight - _minHeight, 0);
            _cells.Enqueue(cell);
        }
    }

    private void OnEnable()
    {
        ItemController.StartFirework += Firework;
    }

    private void OnDisable()
    {
        ItemController.StartFirework -= Firework;
    }

    private void Firework(Color color)
    {
        if (color == _lineColor)
        {
            _firework.Play();
        }
    }
}
