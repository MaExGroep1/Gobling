using System;
using Enums;
using UnityEngine;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private GameObject visuals;        //the parent GameObject of all visuals
        [SerializeField] private float activationSpeed;     //the speed at which the item activates
        [SerializeField] private float deactivationSpeed;   //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed;          //x-axis move speed for JumpToPosition()
        [SerializeField] private float jumpYSpeed;          //y-axis move speed for JumpToPosition()
        [SerializeField] private float jumpHeight;          //jump height for JumpToPosition()
        
        private GameObject _prefab;                         // visuals of the item and extra scripts
        private int _value = 10;                            // the base value of the item
        private ItemType _itemType = ItemType.Normal;       // the type of item
        
        private MinMax<int> _valuePercentage;               // Minimum and Maximum value Percentage

        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        /// <param name="itemName">The name of the Item</param>
        public void Initialize(ItemData itemData, string itemName)
        {
            gameObject.name = itemName;
            _prefab = itemData.prefab;
            _value = itemData.value;
            _itemType = itemData.itemType;
            _valuePercentage = itemData.valuePercentage; 
        }
        
        /// <summary>
        /// Calculates min and max percentage values based on `_value`.
        /// </summary>
        /// <returns>Returns a `MinMax<int>` with calculated values.</returns>
        public MinMax<int> CalculateValuePercent() => new MinMax<int>(_value / _valuePercentage.min, _value / _valuePercentage.max);

        
        /// <summary>
        /// activates the visuals of the item, and scales it from 0 to 1 using a tween
        /// </summary>
        public void Activate()
        {
            visuals.SetActive(true);
            LeanTween.scale(visuals.gameObject, Vector3.one, activationSpeed).setEase(LeanTweenType.easeOutElastic);
        }
        
        /// <summary>
        /// scales the item to 0 using a tween, and deactivates the visuals
        /// </summary>
        public void Deactivate()
        {
            LeanTween.scale(visuals.gameObject, Vector3.zero, deactivationSpeed).setEase(LeanTweenType.easeInElastic).setOnComplete(()=>visuals.SetActive(false));
        }

        /// <summary>
        /// moves the item to a position using two tweens to make it look like it jumps
        /// </summary>
        public void JumpToPosition(Vector3 endPosition)
        {
            var distance = Vector3.Distance(transform.position, endPosition);
            float xDuration = distance / jumpXSpeed;
            float yDuration = distance / jumpYSpeed;

            LeanTween.move(gameObject, endPosition, xDuration).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocalY(visuals.gameObject, jumpHeight, yDuration).setEase(LeanTweenType.easeOutQuint).setLoopPingPong(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Activate();
        }
    }
}
