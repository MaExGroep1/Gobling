using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public class SpawnTestItem : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject jumpPosition;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private LootTable lootTable;
    
    private GameObject _spawnedItem;
    private Items _item;
    private int _currentItem = 0;
    private GameObject _theItem;

    void Start()
    {
        _spawnedItem = Instantiate(itemPrefab, spawnPoint.transform.position, Quaternion.identity);
        _item = _spawnedItem.GetComponent<Items>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_theItem != null) Destroy(_theItem);
            _theItem = Instantiate(lootTable.GetItem(_currentItem).prefab, spawnPoint.transform.position, Quaternion.identity);
            _currentItem++;
            if (lootTable.GetItem(_currentItem) == null) _currentItem = 0;
        }
    }
}
