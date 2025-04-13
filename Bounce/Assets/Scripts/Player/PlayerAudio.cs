using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private List<AudioClip> jumpSounds;
    [SerializeField] private List<AudioClip> landOnGroundSounds;
    [SerializeField] private List<AudioClip> AttackSwingSounds;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource aSJump;
    [SerializeField] private AudioSource aSLand;
    [SerializeField] private AudioSource aSSwing;
    [SerializeField] private AudioSource aSDeath;

    public void PlayAudioPlayerJump(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        AudioClip clip = GetRandomSoundClip(jumpSounds);

        if (clip != null)
        {
            PlaySoundWithRandomPitch(clip, aSJump);
        }
    }

    public void PlayAudioPlayerLand(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        AudioClip clip = GetRandomSoundClip(landOnGroundSounds);

        if (clip != null)
        {
            PlaySoundWithRandomPitch(clip, aSLand);
        }
    }

    public void PlayAudioPlayerSwing(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        AudioClip clip = GetRandomSoundClip(AttackSwingSounds);

        if (clip != null)
        {
            PlaySoundWithRandomPitch(clip, aSSwing);
        }
    }

    public void PlayAudioPlayerDeath(Component sender, object id)
    {
        if (transform.GetComponentInParent<PlayerId>().GetId() != (int)id) { return; }

        if (deathSound != null)
        {
            aSDeath.PlayOneShot(deathSound);
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

    private void PlaySoundWithRandomPitch(AudioClip clip, AudioSource aS)
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
