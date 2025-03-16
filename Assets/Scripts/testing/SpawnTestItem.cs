using Item;
using UnityEngine;

namespace testing
{
    public class SpawnTestItem : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private LootTable lootTable;
    
        private int _currentItem = -1;
        private GameObject _theItem;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow)) return;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_theItem != null) Destroy(_theItem);
                _currentItem++;
                if (lootTable.GetItem(_currentItem) == null) _currentItem = 0;
                _theItem = Instantiate(lootTable.GetItem(_currentItem).prefab, spawnPoint.transform);
            }
            else
            {
                if (_theItem != null) Destroy(_theItem);
                _currentItem--;
                _theItem = Instantiate(lootTable.GetItem(_currentItem).prefab, spawnPoint.transform);
            }
        }
    }
}
