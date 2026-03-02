using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioMixer audioMixer;
    public Dictionary<int, AudioSource> playingSound;
    public AudioMixerGroup musicGroup, sfxGroup;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        ApplySoundSetting();
        audioSource.clip = bgm;
        audioSource.Play();
        playingSound = new Dictionary<int, AudioSource>();
    }

    public void ApplySoundSetting()
    {
        SetValue(SoundType.Music,SettingData.settingData[SETTING.MUSIC]);
        SetValue(SoundType.Sfx,SettingData.settingData[SETTING.SOUND]);
    }

    private void SetValue(SoundType soundType,bool value)
    {
        audioMixer.SetFloat(GetKeyValue(soundType),value ? (int)soundType : -80);
    }

    private string GetKeyValue(SoundType soundType)
    {
        return soundType switch
        {
            SoundType.Music => "music",
            SoundType.Sfx => "sfx",
            _ => "Music",
        };
    }

    public void PlayClickSound()
    {
        PlaySFX(clickSound);
        Debug.Log("Click Sound");
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip == null || playingSound == null) return;

        int id = clip.GetInstanceID();
        if (playingSound.ContainsKey(id))
        {
            playingSound[id].Stop();
            playingSound[id].Play();
        }
        else
        {
            AddAudioSource(clip).Play();
        }
    }

    public AudioSource AddAudioSource(AudioClip sfx, bool isSFX = true)
    {
        AudioSource ac = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        ac.outputAudioMixerGroup = isSFX ? sfxGroup : musicGroup;
        ac.loop = false;
        ac.playOnAwake = false;
        ac.clip = sfx;
        playingSound.Add(sfx.GetInstanceID(), ac);
        return ac;
    }
}

public enum SoundType
{
    Music = 1,
    Sfx = 2
}
