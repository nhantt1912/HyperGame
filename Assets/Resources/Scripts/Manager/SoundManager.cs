using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public SortedList<int, AudioSource> playingSound;
    public AudioMixerGroup musicGroup, sfxGroup;
    [SerializeField] private AudioClip clickSound;
    
    private void Start()
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
            _ => "music",
        };
    }

    public void PlayClickSound(AudioClip clip)
    {
        PlaySFX(clickSound);
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
