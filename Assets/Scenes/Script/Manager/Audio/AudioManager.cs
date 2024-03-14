using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AudioManager : PersistenceSingleton<AudioManager>
{
    private VolumeManager _volumeManager;
    [SerializeField] private AudioSource m_SFXSource;
    [SerializeField] private  AudioSource m_backGroundMusicSource;
    [SerializeField] private AudioClip m_defaultBackGroundMusicClip;
    [SerializeField] private AudioClip m_testSoundClip;
    public float BackGroundRealVolume
    {
        get
        {
            return m_backGroundMusicSource.volume;
        }
        set
        {
            m_backGroundMusicSource.volume = value;
        }
    }
    public float SFXRealVolume 
    {
        get
        {
            return m_SFXSource.volume;
        }
        set
        {
            m_SFXSource.volume = value;
            PlayTestSound();
        }
    }
    public AudioClip m_SFXClip
    {
        get
        {
            return m_SFXSource.clip;
        }
        set
        {
            m_SFXSource.clip = value;
        }
    }
    public AudioClip m_backGroundMusicClip
    {
        get
        {
            return m_backGroundMusicSource.clip;
        }
        set
        {
            m_backGroundMusicSource.clip = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if (!gameObject.TryGetComponent<VolumeManager>(out _volumeManager))
        {
            _volumeManager = gameObject.AddComponent<VolumeManager>();
        }
    } 
    public void PlaySFX(AudioClip sfx)
    {
        m_SFXClip = sfx;
        m_SFXSource.PlayOneShot(sfx);
    }
    public void PlayBackGroundMusic(AudioClip background)
    {
        if (background)
        {
            m_backGroundMusicClip = background;
        }
        else 
        {
            m_backGroundMusicClip = m_defaultBackGroundMusicClip;
        }
        m_backGroundMusicSource.loop = true;
        m_backGroundMusicSource.Play();
    }
    void PlayTestSound()
    {
        m_SFXSource.PlayOneShot(m_testSoundClip);
    }
}
