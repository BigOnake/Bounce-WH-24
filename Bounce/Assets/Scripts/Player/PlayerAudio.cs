using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private List<AudioClip> jumpSounds;
    [SerializeField] private List<AudioClip> landOnGroundSounds;

    private AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
    }

    public void PlayAudioPlayerJump()
    {
        AudioClip clip = GetRandomSoundClip(jumpSounds);

        if (clip != null)
        {
            Debug.Log("Player Jump Audio Play");
            PlaySoundWithRandomPitch(clip);
        }
    }

    public void PlayAudioPlayerLand()
    {
        Debug.Log("Player Land Audio Function Start");
        AudioClip clip = GetRandomSoundClip(landOnGroundSounds);

        if (clip != null)
        {
            Debug.Log("Player Land Audio Play");
            PlaySoundWithRandomPitch(clip);
        }
    }

    public void PlayAudioPlayerDeath()
    {
        if (deathSound != null)
        {
            aS.clip = deathSound;
            aS.Play();
        }
    }

    private AudioClip GetRandomSoundClip(List<AudioClip> clips)
    {
        if (clips.Count != 0)
        {
            return clips[Random.Range(0, clips.Count)];
        }
        else
        {
            return null;
        }
    }

    private void PlaySoundWithRandomPitch(AudioClip clip)
    {
        if (clip != null)
        {
            aS.clip = clip;
            aS.pitch = Random.Range(0.9f, 1.1f);
            aS.Play();
            aS.pitch = 1;
        }
    }
}
