using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private int activeClipIndex = 0;

    private AudioSource audioSource;
    private double pauseClipTime = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = audioClips[activeClipIndex];
        audioSource.Play();
    }

    private void Update()
    {
        if(audioSource.time >= audioClips[activeClipIndex].length)
        {
            activeClipIndex = (activeClipIndex + 1) % audioClips.Length;

            audioSource.clip = audioClips[activeClipIndex];
            audioSource.Play();
        }
    }

    public void PitchThis(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public void OnPauseGame()
    {
        pauseClipTime = audioSource.time;
        audioSource.Pause();
    }

    public void OnResumeGame()
    {
        audioSource.PlayScheduled(pauseClipTime);
        pauseClipTime = 0;
    }
}
