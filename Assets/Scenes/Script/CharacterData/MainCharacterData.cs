using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My project/MainCharacterData")]
public class MainCharacterData : ScriptableObject
{
    [Header("Default data")]
    public int m_maxHealth;
    public int m_Damage;
    [Header("Running related data")]
    public int m_RunSpeed;
    public int m_RunAccel;

}
