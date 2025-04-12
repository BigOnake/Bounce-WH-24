using UnityEngine;

public class BallAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip hitByPlayerSound;
    [SerializeField] private AudioClip surfaceBounceSound;

    public void PlayAudioPlayerHit()
    {
        if (hitByPlayerSound)
            AudioSource.PlayClipAtPoint(hitByPlayerSound, transform.position);
    }

    public void PlayAudioSurfaceHit()
    {
        if (surfaceBounceSound)
            AudioSource.PlayClipAtPoint(surfaceBounceSound, transform.position);
    }


}
