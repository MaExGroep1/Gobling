using System;
using UnityEngine;

namespace Customer
{
    public class CustomerAnimations : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void SetAnimationFloat(string parameterName, float value) => _animator.SetFloat(parameterName, value);
        
        public void TriggerAnimation(string parameterName) => _animator.SetTrigger(parameterName);
    }
}
