using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private GameObject m_Player;

    private EnemyMovement Em;

    [SerializeField] private float m_TimeBetweenShots;
    private float m_ShotTimer;

    [SerializeField] private float m_HitChance;
    [SerializeField] private float m_AttackRange;

    [SerializeField] private bool Attackable;
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("player");
        Em = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        Vector3 dir = transform.position - m_Player.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir,out hit, m_AttackRange))
        {
            if (hit.collider.name == m_Player.name)
            {
                Attackable = true;
            }
        }
        if (Em.m_SpottedTarget == true)
        {
            m_ShotTimer += Time.deltaTime;
            if (m_ShotTimer > m_TimeBetweenShots && Vector3.Distance(transform.position, m_Player.transform.position) < m_AttackRange)
            {
                float Hitchance;
                Hitchance = Random.Range(0, m_HitChance);

                if (Hitchance == 1 && Attackable == true)
                {
                    m_Player.GetComponent<PlayerMovement>().TakeDamage();
                }
            }
        }
    }
}
