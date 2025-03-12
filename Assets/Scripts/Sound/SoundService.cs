using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundService : MonoBehaviour
    {
        [Header("Adio Settings")]
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private float volume;
        
        [Header("Audio Settings")]
        [SerializeField] private Transform soundObjectSpawn;
        [SerializeField] private bool isRadio;
        
        [Header("Radio Settings")]
        [ShowIf("isRadio")]
        [SerializeField] private AudioClip newsClip;
        
        [ShowIf("isRadio")]
        [SerializeField] private AudioClip radioCallClip;
        
        
        private bool _hasFinishedNews;
        private bool _hasSpawned;
        public void FootStep()
        {
            PlayRandomClip();
        }

        private void Start()
        {
            if (!isRadio && _hasSpawned) return;
                _hasSpawned = true;

                StartCoroutine(PlayNewsThenRandom());
        }
        
        private IEnumerator PlayNewsThenRandom()
        {
            SoundManager.PlaySoundClip(radioCallClip, soundObjectSpawn, 0.2f);
            yield return new WaitForSeconds(radioCallClip.length);
            SoundManager.PlaySoundClip(newsClip, soundObjectSpawn, 0.2f);
            yield return new WaitForSeconds(newsClip.length);
            PlayRandomClip();
        }

        private void PlayRandomClip()
        {
            if (audioClips.Length == 0) return;
            
            var randClip = audioClips[Random.Range(0, audioClips.Length)];
            SoundManager.PlaySoundClip(randClip, soundObjectSpawn, volume);
        }
    }
}