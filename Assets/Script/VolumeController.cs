using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public Button musicMuteButton, sfxMuteButton; // Buttons for mute

    // PlayerPrefs keys (same as before)
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_MUTE_KEY = "MusicMute";
    private const string SFX_MUTE_KEY = "SFXMute";

    private void Start()
    {
        LoadAudioSettings();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        PlayerPrefs.SetInt(MUSIC_MUTE_KEY, AudioManager.Instance.musicSource.mute ? 1 : 0);

        // Update button text or visual state based on mute status (optional)
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        PlayerPrefs.SetInt(SFX_MUTE_KEY, AudioManager.Instance.sfxSource.mute ? 1 : 0);

        // Update button text or visual state based on mute status (optional)
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxSlider.value);
    }

    private void LoadAudioSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);

        // Apply loaded mute states to AudioManager and update button visuals
        bool musicMuted = PlayerPrefs.GetInt(MUSIC_MUTE_KEY, 0) == 1;
        bool sfxMuted = PlayerPrefs.GetInt(SFX_MUTE_KEY, 0) == 1;

        AudioManager.Instance.musicSource.mute = musicMuted;
        AudioManager.Instance.sfxSource.mute = sfxMuted;

        UpdateMuteButtonVisuals(musicMuteButton, musicMuted);
        UpdateMuteButtonVisuals(sfxMuteButton, sfxMuted);
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.Save();
    }

    // Helper method to update button visuals based on mute state (you'll need to implement this)
    private void UpdateMuteButtonVisuals(Button button, bool isMuted)
    {
        // Change button text, image, or color based on 'isMuted'
        // ... 
    }
}