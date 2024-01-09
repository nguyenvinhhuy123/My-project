using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    private MainCharacterController m_followTarget;
    private CinemachineVirtualCamera m_virtualCamera;
    private UnityAction<MainCharacterController> m_onCharacterSpawnAction;
    public UnityAction<MainCharacterController> OnCharacterSpawnAction {get {return m_onCharacterSpawnAction;}} 
    void Awake() {
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void OnEnable() {
        m_onCharacterSpawnAction += SetFollowTarget;
    }
    void OnDisable()
    {
        m_onCharacterSpawnAction -= SetFollowTarget;  
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.OnCharacterSpawnListenerRegister(OnCharacterSpawnAction);
    }
    void OnDestroy()
    {
        PlayerManager.Instance.OnCharacterSpawnListenerUnRegister(OnCharacterSpawnAction);
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
