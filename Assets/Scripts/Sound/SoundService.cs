using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundService : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private float volume;
        
        [Header("Audio Settings")]
        [SerializeField] private Transform soundObjectSpawn;
        [SerializeField] private bool isRadio;
        
        [Header("Radio Settings")]
        
        [ShowIf("isRadio")]
        [SerializeField] private AudioClip radioCallClip;
        
        
        private bool _hasFinishedNews;
        private bool _hasSpawned;
        public void FootStep()
        {
            SoundManager.PlayRandomClip(audioClips, soundObjectSpawn, volume);
        }

        private void Start()
        {
            if (!isRadio && _hasSpawned) return;
                _hasSpawned = true;

                StartCoroutine(PlayNewsThenRandom());
        }
        
        protected IEnumerator PlayNewsThenRandom()
        {
            SoundManager.PlaySoundClip(radioCallClip, soundObjectSpawn, volume);
            yield return new WaitForSeconds(radioCallClip.length);
            SoundManager.PlayRandomClip(audioClips, soundObjectSpawn, volume);
        }


    }
}