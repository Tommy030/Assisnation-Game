using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private GameObject m_Player;

    private enemyState Es;

    [SerializeField] private float m_TimeBetweenShots;
    private float m_ShotTimer;

    [SerializeField] private int m_HitChance;
    [SerializeField] private float m_AttackRange;
    [SerializeField] private float m_Damage = 10;

    [SerializeField] private bool Attackable;
    [SerializeField] private ParticleSystem m_MuzzleFlash;
    void Start()
    {
        m_Player = GameObject.Find("player");
        Es = GetComponent<enemyState>();
    }

    void Update()
    {
        if (Es.dead)
        {
            this.enabled = false;
        }
        Vector3 dir = m_Player.transform.position - transform.position;

        //Debug.DrawLine(transform.position, dir, Color.red);
        Debug.DrawRay(transform.position, dir, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir,out hit, m_AttackRange))
        {
            //Debug.Log("de hit: " + hit.collider.name + "player: " + m_Player.name);
            if (hit.collider.name == m_Player.name)
            {
                //Debug.Log("ping1");
                Attackable = true;
            }
        }
        if (Es.hunting == true)
        {
            m_ShotTimer += Time.deltaTime;
            if (m_ShotTimer > m_TimeBetweenShots && Vector3.Distance(transform.position, m_Player.transform.position) < m_AttackRange)
            {
                m_MuzzleFlash.Play();
                int Hitchance;
                Hitchance = Random.Range(1, m_HitChance + 1);
                Debug.Log("ping en git:" + Hitchance);

                if (Hitchance == 1 && Attackable == true)
                {
                    m_Player.GetComponent<PlayerMovement>().TakeDamage(m_Damage);
                    Debug.Log("ping");
                }
                m_ShotTimer = 0;
            }
        }
    }
}
