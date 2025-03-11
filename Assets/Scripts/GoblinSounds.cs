using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;

public class GoblinSounds : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    public void FootStep()
    {
        SoundManager.PlaySoundClip(audioClip, 10);
    }
}
