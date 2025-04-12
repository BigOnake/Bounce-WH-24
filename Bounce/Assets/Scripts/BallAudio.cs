using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip hitByPlayerSound;
    [SerializeField] private List<AudioClip> surfaceBounceSounds;

    public void PlayAudioPlayerHit()
    {
        if (hitByPlayerSound)
            //AudioSource.PlayClipAtPoint(hitByPlayerSound, transform.position);
            PlaySoundWithRandomPitch(hitByPlayerSound);
    }

    public void PlayAudioSurfaceHit()
    {
        AudioClip clip = GetRandomSoundClip(surfaceBounceSounds);
        if (clip != null)
            //AudioSource.PlayClipAtPoint(clip, transform.position);
            PlaySoundWithRandomPitch(clip);
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
            GameObject gO = new GameObject("TempAudio");
            gO.transform.position = transform.position;

            AudioSource aS = gO.AddComponent<AudioSource>();
            aS.clip = clip;
            aS.pitch = Random.Range(0.9f, 1.1f);
            aS.spatialBlend = 1.0f;
            aS.Play();

            Destroy(gO, clip.length / aS.pitch);
        }
    }
}
