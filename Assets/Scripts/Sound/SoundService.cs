using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    public void FootStep()
    {
        var randClip = _audioClips[Random.Range(0, _audioClips.Length)];
        SoundManager.PlaySoundClip(randClip);
    }
}