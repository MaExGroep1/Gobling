using System;
using UnityEngine;

namespace Customer
{
    public class CustomerAnimations : MonoBehaviour
    {
        private Animator _animator;                         // the animator of the customer
        public bool itemCanJump { get; private set; }              // weather the item can jump
        public bool canLeaveShop { get; private set; }        // weather the customer can leave the shop
        
        /// <summary>
        /// Set the animator as the gameobjects animator
        /// </summary>
        private void Awake() => _animator = GetComponent<Animator>();
        
        public void CanJump() => itemCanJump = true;

        public void CantJump() => itemCanJump = false;
        
        public void CanLeaveShop() => canLeaveShop = true;
        
        public void CantLeaveShop() => canLeaveShop = false;
        
        public void SetSpeedMultiplierFloat(float value) => _animator.SetFloat("SpeedMultiplier", value);
        
        public void TriggerAtCounter() => _animator.SetTrigger("AtCounter");
        
        public void TriggerLeaveCounter() => _animator.SetTrigger("LeaveCounter");
        
        public void TriggerGiveTake() => _animator.SetTrigger("GiveTake");
        
        public void TriggerYes() => _animator.SetTrigger("Yes");
        
        public void TriggerNo() => _animator.SetTrigger("No");
        
        public void SetIsTalking(bool set) => _animator.SetBool("IsTalking", set);
    }
}
