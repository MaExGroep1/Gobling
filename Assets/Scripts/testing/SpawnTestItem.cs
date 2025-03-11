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
    
    private GameObject _spawnedItem;
    private Items _item;
    void Start()
    {
        _spawnedItem = Instantiate(itemPrefab, spawnPoint.transform.position, Quaternion.identity);
        _item = _spawnedItem.GetComponent<Items>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) _item.Activate();
        if (Input.GetKeyDown(KeyCode.Backspace)) _item.Deactivate();
        if (Input.GetKeyDown(KeyCode.Return)) _item.JumpToPosition(jumpPosition.transform.position);
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) _item.JumpToPosition(spawnPoint.transform.position);*/
    }
}
