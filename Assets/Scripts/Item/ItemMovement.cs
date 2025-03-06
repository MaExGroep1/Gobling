using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class ItemMovement : MonoBehaviour
    {
        [SerializeField] private GameObject itemParent; //the parent GameObject of all visuals
        [SerializeField] private float activationSpeed; //the speed at which the item activates
        [SerializeField] private float deactivationSpeed; //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed; //x-axis move speed for JumpToPosition()
        [SerializeField] private float jumpYSpeed; //y-axis move speed for JumpToPosition()
        [SerializeField] private float jumpHeight; //jump height for JumpToPosition()
        [SerializeField] private float jumpWaitTime;
        [SerializeField] private float deactivationWaitTime;

        /// <summary>
        /// activates the visuals of the item, and scales it from 0 to 1 using a tween
        /// </summary>
        ///
        ///
        private void Awake()
        {
            ItemManager.OnEnableAndJump += ActivateAndJump;
            ItemManager.OnJumpAndDisable += JumpAndDeactivate;
        }

        private void OnDestroy()
        {
            ItemManager.OnEnableAndJump -= ActivateAndJump;
            ItemManager.OnJumpAndDisable -= JumpAndDeactivate;
        }
        
        
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
        private void ActivateAndJump(Items item, Vector3 jumpPosition, Vector3 startPosition)
        {
            if (item.gameObject != gameObject) return; 
            
            if (startPosition != default) gameObject.transform.position = startPosition;
            StartCoroutine(JumpAndActivateTimer(jumpPosition));
        }
        
        private void JumpAndDeactivate(Items item, Vector3 jumpPosition, Vector3 startPosition)
        {
            if (item.gameObject != gameObject) return; 
            
            if (startPosition != default) gameObject.transform.position = startPosition;
            StartCoroutine(DeactivateAndJumpTimer(jumpPosition));
        }

        private IEnumerator JumpAndActivateTimer(Vector3 jumpPosition)
        {
            Activate();
            yield return new WaitForSeconds(jumpWaitTime);
            JumpToPosition(jumpPosition);
        }
        
        private IEnumerator DeactivateAndJumpTimer(Vector3 jumpPosition)
        {
            JumpToPosition(jumpPosition);
            yield return new WaitForSeconds(deactivationWaitTime);
            Deactivate();
        }
    }
}
