﻿using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    void Awake()
    {
        Time.timeScale = 1.0f; 

        // Initialize audio sources
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

        // If slideres exist, add onValueChange listeners
        if (masterVolumeSlider)
            masterVolumeSlider.onValueChanged.AddListener(delegate { MasterVolumeChange(); });
       
        if (musicVolumeSlider)
            musicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        
        if (effectsVolumeSlider)
            effectsVolumeSlider.onValueChanged.AddListener(delegate { EffectsVolumeChange(); });

        // Set audio slider defaults or load up saved settings from PlayerPrefs
        if (masterVolumeSlider && musicVolumeSlider && effectsVolumeSlider)
        {
            if (!PlayerPrefs.HasKey("MasterVolume"))
                masterVolumeSlider.value = 1f;
            else
                masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");

            if (!PlayerPrefs.HasKey("MusicVolume"))
                musicVolumeSlider.value = 0.325f;
            else
                musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            if (!PlayerPrefs.HasKey("EffectsVolume"))
                effectsVolumeSlider.value = 1f;
            else
                effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        }
        Play("Music");
    }

    // Plays the sound source itself. Repeated calls will reset the sound being played.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
            s.source.Play();
    }

    // Plays an instance of the sound (the clip of the source). Repeated calls will produce multiple instances of the sound. 
    public void PlayInstance(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.PlayOneShot(s.source.clip);
    }

    // Stops all instances of the named sound source
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
            s.source.Stop();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            return s.source.isPlaying;
        return false;
    }

    // onValueChanged functions (NULL safe)
    private void MasterVolumeChange()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);

        foreach (Sound s in sounds)
        {
            if (s.soundType == Sound.SoundType.Music)
                s.source.volume = masterVolumeSlider.value * musicVolumeSlider.value;

            if (s.soundType == Sound.SoundType.Effect)
                s.source.volume = masterVolumeSlider.value * effectsVolumeSlider.value;
        }
    }

    private void MusicVolumeChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);

        foreach (Sound s in sounds)
        {
            if (s.soundType == Sound.SoundType.Music)
                s.source.volume = masterVolumeSlider.value * musicVolumeSlider.value;
        }
    }

    private void EffectsVolumeChange()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);

        foreach (Sound s in sounds)
        {
            if(s.soundType == Sound.SoundType.Effect)
                s.source.volume = masterVolumeSlider.value * effectsVolumeSlider.value;
        }
        Play("Star");
    }
}