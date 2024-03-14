using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    const float SCALE_DOWN_VALUE = 0.01f;
    /*volume's value input will be in scale of 100 because how slider work
    thus we want to scale down to scale of 1 to match how audio source behave
    */
    private AudioManager _audioManager = AudioManager.Instance;
    [SerializeField, Range(0,1f)] private float m_masterVolume = 1f;
    [SerializeField, Range(0,1f)] private float m_backGroundVolume = 1f;
    [SerializeField, Range(0,1f)] private float m_SFXVolume = 1f;
    public float BackGroundVolume 
    {
        get {return m_backGroundVolume;}
        set
        {
            m_backGroundVolume = value*SCALE_DOWN_VALUE;
            OnVolumeChange();
        }
    }
    public float SFXVolume 
    {
        get {return m_SFXVolume;}
        set
        {
            m_SFXVolume = value*SCALE_DOWN_VALUE;
            OnVolumeChange();
        }
    }
    public float MasterVolume 
    {
        get {return m_masterVolume;}
        set 
        {
            m_masterVolume = value*SCALE_DOWN_VALUE;
            OnVolumeChange();
        }
    }
    void Awake() {
        VolumeInit();
    }
    void OnVolumeChange()
    {
        VolumeRecalc();
    }
    void VolumeRecalc()
    {
        float realBGVolume = m_backGroundVolume*m_masterVolume;
        float realSFXVolume = m_SFXVolume*m_masterVolume;
        _audioManager.BackGroundRealVolume = realBGVolume;
        _audioManager.SFXRealVolume = realSFXVolume;
    }
    void VolumeInit()
    {
        //TODO: Initialize volume base on saved data
        m_masterVolume = 1f;
        m_backGroundVolume = 1f;
        m_SFXVolume = 1f;
    }
    void OnValidate()
    {
        //*call volume change on validate serialize data */
        OnVolumeChange();
    }
}
