using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
abstract public class VolumeUIController : MonoBehaviour
{
    protected VolumeManager m_volumeManager;
    protected Slider m_sliderBar;
    // Start is called before the first frame update
    void Start()
    {
        m_sliderBar = gameObject.GetComponent<Slider>();
        m_volumeManager = AudioManager.Instance.VolumeManager;
    }

    abstract public void OnValueChanged();
    public void PlayTestSound()
    {
        AudioManager.Instance.PlayTestSound();
    }
}
