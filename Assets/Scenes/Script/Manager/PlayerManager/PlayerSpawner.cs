using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_mainCharacterPrefab;
    [SerializeField] private float m_reSpawnWaitTime; 
    public float ReSpawnWaitTime {get {return m_reSpawnWaitTime;}}
    private CheckPointController _lastCheckPoint;
    public Vector3 LastCheckPoint {get {return _lastCheckPoint.Position;}}
    private readonly string START_POINT_TAG = "CheckPoint/Start";
    // Start is called before the first frame update
    void Awake() 
    {
    }
    void Start()
    {
        //*Find Start Point to spawn character later */
        _lastCheckPoint = FindStartPoint();
        SpawnNewCharacterInstance();
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
        Debug.Log("Wait for re-spawn");
        yield return new WaitForSeconds(m_reSpawnWaitTime);
        SpawnNewCharacterInstance();
        yield return null;
    }
    private void SpawnNewCharacterInstance()
    {
        Instantiate(m_mainCharacterPrefab, _lastCheckPoint.Position, Quaternion.identity)
        .TryGetComponent<MainCharacterController>(out MainCharacterController m_characterInstance);
    }
    private CheckPointController FindStartPoint()
    {
        CheckPointController startPoint = 
        GameObject.FindGameObjectWithTag(START_POINT_TAG)
        .GetComponent<CheckPointController>();
        return startPoint;
    }
    public void OnChecked(CheckPointController checkPoint)
    {
        _lastCheckPoint = checkPoint;
    }
}
