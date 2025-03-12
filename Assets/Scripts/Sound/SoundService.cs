using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundService : MonoBehaviour
    {
        [Header("Adio Settings")]
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private float volume;
        
        [Header("Adio Settings")]
        [SerializeField] private Transform soundObjectSpawn;
        [SerializeField] private bool isRadio;
        
        private bool _hasSpawned;
        public void FootStep()
        {
            PlayRandomClip();
        }

        private void Start()
        {
            if (isRadio && !_hasSpawned)
            {
                PlayRandomClip();
                _hasSpawned = true;
            }
        }

        private void PlayRandomClip()
        {
            if (audioClips.Length == 0) return;
            
            var randClip = audioClips[Random.Range(0, audioClips.Length)];
            SoundManager.PlaySoundClip(randClip, soundObjectSpawn, volume);
        }
    }
}