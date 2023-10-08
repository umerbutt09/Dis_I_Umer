using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public AudioSource AudioPlayer;
    [Header("AudioClips")]
    public AudioClip ClickSound;
    public AudioClip WrongSound;
    public AudioClip CorrectSound;
    public AudioClip LoseSound;
    public AudioClip WinSound;

    public void PlayClickSound ()
    {
        AudioPlayer.pitch = Random.Range(0.8f, 1.2f);
        AudioPlayer.PlayOneShot(ClickSound);
        AudioPlayer.pitch = 1.0f;
    }

    public void PlayWrongSound ()
    {
        AudioPlayer.pitch = Random.Range(0.8f, 1.2f);
        AudioPlayer.volume = 0.27f;
        AudioPlayer.PlayOneShot(WrongSound);
        AudioPlayer.pitch = 1.0f;
        AudioPlayer.volume = 1.0f;
    }

    public void PlayCorrectSound ()
    {
        AudioPlayer.pitch = Random.Range(0.8f, 1.2f);
        AudioPlayer.PlayOneShot(CorrectSound);
        AudioPlayer.pitch = 1.0f;
    }

    public void PlayWinSound ()
    {
        AudioPlayer.PlayOneShot(WinSound);
    }

    public void PlayLoseSound ()
    {
        AudioPlayer.PlayOneShot(LoseSound);
    }


}
