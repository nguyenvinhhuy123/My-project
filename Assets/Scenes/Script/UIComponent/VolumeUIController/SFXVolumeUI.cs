using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolumeUI : VolumeUIController
{
    override public void OnValueChanged()
    {
        m_volumeManager.SFXVolume = m_sliderBar.value;
    }
}
