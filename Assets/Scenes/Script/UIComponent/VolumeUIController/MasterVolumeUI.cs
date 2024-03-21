using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeUI : VolumeUIController
{
    override public void OnValueChanged()
    {
        m_volumeManager.MasterVolume = m_sliderBar.value;
    }
    
}
