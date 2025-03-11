using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        public string ItemName;
        [SerializeField] private GameObject visuals;        //the parent GameObject of all visuals
        [SerializeField] private float activationSpeed;     //the speed at which the item activates
        [SerializeField] private float deactivationSpeed;   //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed;          //x-axis move speed for JumpToPosition()
        [SerializeField] private float jumpYSpeed;          //y-axis move speed for JumpToPosition()
        [SerializeField] private float jumpHeight;          //jump height for JumpToPosition()
        [SerializeField] private MinMax<int> barValues;     // Minimum and Maximum value Percentage
        
        private GameObject _prefab;                         //visuals of the item and extra scripts
        private ItemType _itemType = ItemType.Normal;       //the type of item
        public int value { get; private set; } = 10;        //the base value of the item
        
        
        

        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        /// <param name="itemName">The name of the Item</param>
        public void Initialize(ItemData itemData, string itemName)
        {
            ItemName = itemData.name;
            gameObject.name = itemName;
            _prefab = itemData.prefab;
            value = itemData.value;
            _itemType = itemData.itemType;
            barValues = itemData.barValues; 
        }
        
        public MinMax<int> barValue => barValues;

        
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
    }
}
