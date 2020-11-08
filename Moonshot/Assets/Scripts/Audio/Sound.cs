using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public enum SoundType { Music, Effect}
    public SoundType soundType;
    public bool loop;

    //[Range(0f, 1f)]
    //public float pitch;
    //public bool bypassEffects;

    [HideInInspector]
    public AudioSource source;
}
