using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy", menuName ="Create new Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public string EnemyName = "Default";
    [Header("Movement")]
    public float m_Speed = 3;    
    public float m_AccelerationSpeed = 1;

    public float m_WanderRange = 10;
    public float m_DurationWaitingAtWanderingLocation = 5;

    [Header("Damage")]
    public float m_Damage = 1;
    
}
