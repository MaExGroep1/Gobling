using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

namespace Customer
{
    public class CustomerAutomaticDoor : MonoBehaviour
    {
        [SerializeField] private GameObject door;       // the GameObject of the door
        [SerializeField] private float doorOpen;        // the angle of the door opening
        [SerializeField] private float doorTime;        // the time it takes for the door to open
        [SerializeField] private float doorWaitTime;    // the time the door waits before closing
        
        /// <summary>
        /// Opens the door then starts to wait
        /// </summary>
        /// <param name="other">The collider of the other object</param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Customer")) return;
            var speedMod = other.GetComponent<CustomerBehaviour>().speed;
            LeanTween.rotateAround(door, Vector3.up,doorOpen, doorTime / speedMod).setEase(LeanTweenType.easeInOutQuad);
            StartCoroutine(DoorWait(speedMod));
            SoundManager.Instance.OnDoorSound();
        }
        
        /// <summary>
        /// Waits doorWaitTime amount of seconds then closes the door
        /// </summary>
        /// <returns></returns>
        private IEnumerator DoorWait(float speedMod)
        {
            yield return new WaitForSeconds(doorWaitTime / speedMod);
            LeanTween.rotateAround(door, Vector3.up,-doorOpen, doorTime / speedMod).setEase(LeanTweenType.easeInOutQuad);
            SoundManager.Instance.OnDoorSound();
        }
    }
}
