using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    public AudioClip bossHit;
    public AudioClip krisztianHit;
    public AudioClip doStop;
    public AudioClip drinking;
    public AudioClip laughing;
    public AudioClip exploSound;
    public float soundInterval = 40f;
    public float soundTimer;

    public void playSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void Start()
    {
        soundTimer = soundInterval;
    }
    public void Update()
    {
        playThatSound();
    }

    private void playThatSound()
    {
        soundTimer -= Time.deltaTime; // Reduziere den Timer

        if (soundTimer <= 0)
        {
            playBossSound(); // Wirf einen Schraubenschlüssel
            soundTimer = soundInterval; // Setze den Timer zurück
        }
    }

    private void playBossSound()
    {
        playSound(doStop);
    }
}
