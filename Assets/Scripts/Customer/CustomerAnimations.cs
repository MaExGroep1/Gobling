using System;
using UnityEngine;

namespace Customer
{
    public class CustomerAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void SetSpeedFloat(float value) => _animator.SetFloat("SpeedMultiplier", value);
        
        public void TriggerAtCounter() => _animator.SetTrigger("AtCounter");
        
        public void TriggerLeaveCounter() => _animator.SetTrigger("LeaveCounter");
        
        public void TriggerGiveTake() => _animator.SetTrigger("GiveTake");
        
        public void TriggerYes() => _animator.SetTrigger("Yes");
        
        public void TriggerNo() => _animator.SetTrigger("No");
        
        public void SetIsTalking(bool set) => _animator.SetBool("IsTalking", set);
    }
}
