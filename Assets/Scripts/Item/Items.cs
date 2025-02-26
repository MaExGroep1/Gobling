using Enums;
using UnityEngine;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private GameObject visuals;        //the parent object of all visuals
        [SerializeField] private float activationSpeed;     //the speed at which the item activates
        [SerializeField] private float deactivationSpeed;   //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed;          
        [SerializeField] private float jumpYSpeed;
        [SerializeField] private float jumpHeight;
        
        private GameObject _prefab;                         // visuals of the item and extra scripts
        private int _value = 10;                            // the base value of the item
        private ItemType _itemType = ItemType.Normal;       // the type of item
        
        private MinMax<int> _valuePercentage;               // Minimum and Maximum value Percentage
        
        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        public void Initialize(ItemData itemData)
        {
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

        public void Activate()
        {
            visuals.SetActive(true);
            LeanTween.scale(visuals.gameObject, Vector3.one, 0.75f).setEase(LeanTweenType.easeOutElastic);
        }
        public void Deactivate()
        {
            LeanTween.scale(visuals.gameObject, Vector3.zero, 0.50f).setEase(LeanTweenType.easeInElastic).setOnComplete(()=>visuals.SetActive(false));
        }

        public void JumpToPosition(Vector3 endPosition)
        {
            var distance = Vector3.Distance(transform.position, endPosition);
            float xDuration = distance / jumpXSpeed;
            float yDuration = distance / jumpYSpeed;

            LeanTween.move(gameObject, endPosition, xDuration).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocalY(visuals.gameObject, jumpHeight, yDuration).setEase(LeanTweenType.easeOutQuint).setLoopPingPong(1);
        }

        
    }
}
