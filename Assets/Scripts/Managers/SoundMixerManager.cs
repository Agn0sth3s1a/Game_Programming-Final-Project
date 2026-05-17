using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;

    public void SetMasterVolume(float level)
    {
        Mixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        Mixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }    

    public void SetSFXVolume(float level)
    {
        Mixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }
}
