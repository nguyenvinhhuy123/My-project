using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeUI : VolumeUIController
{
    override public void OnValueChanged()
    {
        m_volumeManager.BackGroundVolume = m_sliderBar.value;
    }
}
