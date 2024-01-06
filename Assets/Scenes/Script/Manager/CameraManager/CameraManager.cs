using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private MainCharacterController m_followTarget;
    private CinemachineVirtualCamera m_virtualCamera;
    void Awake() {
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetFollowTarget(MainCharacterController character)
    {
        if (m_followTarget) return;
        m_followTarget = character;

        m_virtualCamera.Follow = m_followTarget.gameObject.transform;
    }
}
