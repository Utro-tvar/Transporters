using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnCall : MonoBehaviour
{
    [SerializeField] private ItemSpawner[] _spawners;
    [SerializeField] private float _cellHeight;

    private float _distance;

    private void Start()
    {
        _distance = _cellHeight * GameInfo.Instanse.ItemInterval - Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float interval = _cellHeight * GameInfo.Instanse.ItemInterval;
        if (_distance >= interval)
        {
            _distance -= interval;
            Spawn();
        }
        _distance += GameInfo.Instanse.Speed * Time.fixedDeltaTime;
    }

    private void Spawn()
    {
        _spawners[Random.Range(0, _spawners.Length)].Spawn();
    }

    //private IEnumerator SpawnCall()
    //{
    //    for (; ; )
    //    {
    //        int index = Random.Range(0, _spawners.Length);
    //        _spawners[index].Spawn();
    //        yield return new WaitForSeconds(_cellHeight * GameInfo.Instanse.ItemInterval / GameInfo.Instanse.Speed);
    //    }
    //}
}
