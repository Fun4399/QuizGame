using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Clips")]
    public AudioClip buttonClick;
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip background;

    void Start()
    {
        // Load saved volume
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;

        // Listen for slider changes
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // ================= MUSIC =================
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // ================= SFX =================
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // ================= PLAY SFX =================
    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClick);
    }

    public void PlayCorrect()
    {
        sfxSource.PlayOneShot(correctAnswer);
    }

    public void PlayWrong()
    {
        sfxSource.PlayOneShot(wrongAnswer);
    }

    public void PlayWin()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayLose()
    {
        sfxSource.PlayOneShot(loseSound);
    }
}