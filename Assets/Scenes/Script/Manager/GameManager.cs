using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : PersistenceSingleton<GameManager>
{
    [SerializeField] private GameObject m_mainCharacterPrefab;
    private MainCharacterController m_characterInstance;
    public MainCharacterController CharacterInstance {get{ return m_characterInstance;}}
    [SerializeField] private int m_characterLife;
    public int CharacterLife {get {return m_characterLife;}}
    [SerializeField] private float m_reSpawnWaitTime; 
    public float ReSpawnWaitTime {get {return m_reSpawnWaitTime;}}
    private Vector3 _lastCheckPoint;
    public Vector3 LastCheckPoint {get {return _lastCheckPoint;}}
    protected override void Awake() {
        base.Awake();

        //!hard code checkpoint
        _lastCheckPoint = new Vector3(-4f, 3f, 0f);
    }
    private void Start() {
        StartCoroutine(nameof(ReSpawnWaitRoutine));
    }
    /// <summary>
    /// Callback for character onDestroy
    /// </summary>
    public void OnCharacterDestroy()
    {
        StartCoroutine(nameof(ReSpawnWaitRoutine));
    }
    private IEnumerator ReSpawnWaitRoutine()
    {
        Debug.Log("Coroutine");
        yield return new WaitForSeconds(m_reSpawnWaitTime);
        SpawnNewCharacterInstance();
    }
    private void SpawnNewCharacterInstance()
    {
        Instantiate(m_mainCharacterPrefab, _lastCheckPoint, Quaternion.identity)
        .TryGetComponent<MainCharacterController>(out MainCharacterController m_characterInstance);
    }

}
