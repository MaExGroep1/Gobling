using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class ItemMovement : MonoBehaviour
    {
        [SerializeField] private GameObject itemParent;         //the parent GameObject of all visuals
        [SerializeField] private float activationSpeed;         //the speed at which the item activates
        [SerializeField] private float deactivationSpeed;       //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed;              //x-axis move speed for JumpToPosition()
        [SerializeField] private float jumpYSpeed;              //y-axis move speed for JumpToPosition()
        [SerializeField] private float jumpHeight;              //jump height for JumpToPosition()
        [SerializeField] private float jumpWaitTime;            //the time the item waits between enabling and jumping
        [SerializeField] private float deactivationWaitTime;    //the time the item waits between jumping and disabling
        
        private void Awake()
        {
            ItemManager.Instance.OnEnableAndJump += ActivateAndJump;
            ItemManager.Instance.OnJumpAndDisable += JumpAndDeactivate;
        }

        private void OnDestroy()
        {
            ItemManager.Instance.OnEnableAndJump -= ActivateAndJump;
            ItemManager.Instance.OnJumpAndDisable -= JumpAndDeactivate;
        }
        
        /// <summary>
        /// activates the visuals of the item, and scales it from 0 to 1 using a tween
        /// </summary>
        public void Activate()
        {
            itemParent.SetActive(true);
            LeanTween.scale(itemParent.gameObject, Vector3.one, activationSpeed).setEase(LeanTweenType.easeOutElastic);
        }

        /// <summary>
        /// scales the item to 0 using a tween, and deactivates the visuals
        /// </summary>
        public void Deactivate()
        {
            LeanTween.scale(itemParent.gameObject, Vector3.zero, deactivationSpeed).setEase(LeanTweenType.easeInElastic)
                .setOnComplete(() => itemParent.SetActive(false));
        }

        /// <summary>
        /// moves the item to a position using two tweens to make it look like it jumps
        /// </summary>
        public void JumpToPosition(Vector3 endPosition, Vector3 startPosition = default)
        {
            if (startPosition != default) gameObject.transform.position = startPosition;
            
            var distance = Vector3.Distance(transform.position, endPosition);
            float xDuration = distance / jumpXSpeed;
            float yDuration = distance / jumpYSpeed;

            LeanTween.move(gameObject, endPosition, xDuration).setEase(LeanTweenType.easeOutQuint);
            LeanTween.moveLocalY(itemParent.gameObject, jumpHeight, yDuration).setEase(LeanTweenType.easeOutQuint)
                .setLoopPingPong(1);
        }
        
        /// <summary>
        /// calls the coroutine used for enabling and jumping the item to a location
        /// if starting position is default(0, 0, 0), the item will jump from its current position
        /// </summary>
        /// <param name="item">used to see if item is this, if it is it will continue</param>
        /// <param name="jumpPosition">the position where the item will jump</param>
        /// <param name="startPosition">the position from which the item will jump</param>
        private void ActivateAndJump(Items item, Vector3 jumpPosition, Vector3 startPosition)
        {
            if (item.gameObject != gameObject) return; 
            
            if (startPosition != default) gameObject.transform.position = startPosition;
            StartCoroutine(JumpAndActivateTimer(jumpPosition));
        }
        
        /// <summary>
        /// calls the coroutine used for jumping  to a certain location and disabling it
        /// if starting position is default(0, 0, 0), the item will jump from its current position
        /// </summary>
        /// <param name="item">used to see if item is this, if it is it will continue</param>
        /// <param name="jumpPosition">the position where the item will jump</param>
        /// <param name="startPosition">the position from which the item will jump</param>
        private void JumpAndDeactivate(Items item, Vector3 jumpPosition, Vector3 startPosition)
        {
            if (item.gameObject != gameObject) return; 
            
            if (startPosition != default) gameObject.transform.position = startPosition;
            StartCoroutine(DeactivateAndJumpTimer(jumpPosition));
        }

        /// <summary>
        /// Activates itself, waits for "jumpWaitTime" and then jumps to the location
        /// </summary>
        /// <param name="jumpPosition"></param>
        /// <returns></returns>
        private IEnumerator JumpAndActivateTimer(Vector3 jumpPosition)
        {
            Activate();
            yield return new WaitForSeconds(jumpWaitTime);
            JumpToPosition(jumpPosition);
        }
        
        /// <summary>
        /// Jumps to the location, waits for "deactivationWaitTime" and deactivates itself
        /// </summary>
        /// <param name="jumpPosition"></param>
        /// <returns></returns>
        private IEnumerator DeactivateAndJumpTimer(Vector3 jumpPosition)
        {
            JumpToPosition(jumpPosition);
            yield return new WaitForSeconds(deactivationWaitTime);
            Deactivate();
        }
    }
}
