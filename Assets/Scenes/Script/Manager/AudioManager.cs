using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AudioManager : PersistenceSingleton<AudioManager>
{
    [SerializeField] private AudioSource m_SFXSource;
    [SerializeField] private  AudioSource m_backGroundMusicSource;
    [SerializeField] private AudioClip m_defaultBackGroundMusicClip;
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
    } 
    public void PlaySFX(AudioClip sfx)
    {
        m_SFXClip = sfx;
        m_SFXSource.PlayOneShot(sfx,1);
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

}
