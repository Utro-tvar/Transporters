using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private static Stack<GameObject> _itemsPool;

    private Color _lineColor;
    private Collider2D _spriteCollider;
    private Vector3 _spawnPosition;

    public void Spawn()
    {
        GameObject item;
        if(_itemsPool.Count > 0)
        {
            item = _itemsPool.Pop();
            item.transform.position = _spawnPosition;
            item.SetActive(true);
        }
        else
        {
            item = Instantiate(GameInfo.Instanse.Setting.ItemPrefab, _spawnPosition, Quaternion.identity);
        }
        SpriteRenderer itemRenderer = item.GetComponent<SpriteRenderer>();
        int ind = Random.Range(0, 4);
        itemRenderer.sprite = GameInfo.Instanse.Lines[ind].ItemSprites[Random.Range(0, GameInfo.Instanse.Lines[ind].ItemSprites.Length)];
        itemRenderer.color = GameInfo.Instanse.Lines[ind].Color;
        item.GetComponent<ItemController>().Init(itemRenderer.color, _lineColor);
    }

    private void Start()
    {
        _spriteCollider = GetComponent<Collider2D>();
        int index = GetComponent<LineController>().Index;
        _lineColor = GameInfo.Instanse.Lines[index].Color;
        _itemsPool = new Stack<GameObject>();
        _spawnPosition = new Vector3(transform.position.x, GameInfo.Instanse.TopEdge, 0);
    }

    private void OnEnable()
    {
        ItemController.ItemDowned += ItemDowned;
        ItemController.EndLife += ItemToPool;
    }

    private void OnDisable()
    {
        ItemController.ItemDowned -= ItemDowned;
        ItemController.EndLife -= ItemToPool;
    }

    private void ItemDowned(ItemController item)
    {
        Vector3 position = item.transform.position;
        if (_spriteCollider.OverlapPoint(new Vector2(position.x, position.y)))
        {
            item.transform.position = new Vector3(transform.position.x, item.transform.position.y, 0);
            item.SetLineColor(_lineColor);
        }
    }

    private void ItemToPool(ItemController item)
    {
        _itemsPool.Push(item.gameObject);
        item.gameObject.SetActive(false);
    }
}
