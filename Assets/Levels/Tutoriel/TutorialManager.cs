using UnityEngine;
using UnityEngine.Audio;

public class TutorialManager : MonoBehaviour
{

    public AudioClip[] AudioClips;

    protected int clipIndex = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayNextClip();

        Init();
    }

    protected virtual void Init()
    {
    }

    void Update()
    {
    }

    protected void PlayNextClip()
    {
        audioSource.clip = AudioClips[clipIndex];
        audioSource.Play();
        clipIndex += 1;
    }

    protected void PlayNextClip(float delay)
    {
        audioSource.clip = AudioClips[clipIndex];
        audioSource.PlayDelayed(delay);
        clipIndex += 1;
    }
}
