using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public ScriptableEnemy m_PublicEnemy;

    private Target t;
    private enemyState es;
    [SerializeField] private Animator m_Animator;
    private NavMeshAgent m_NavMesh;
    private CapsuleCollider m_Collider;

    private float m_CurrentHealth;
    [SerializeField] private GameObject m_VisionCone;
    private void Start()
    {
        t = gameObject.GetComponent<Target>();
        es = gameObject.GetComponent<enemyState>();
        m_CurrentHealth = m_PublicEnemy.Health;
        m_NavMesh = gameObject.GetComponent<NavMeshAgent>();
        m_Collider = gameObject.GetComponent<CapsuleCollider>();
    }
    public void RemoveHP(int damage)
    {
        Debug.Log("hit");
        es.hunting = true;
        if (m_CurrentHealth - damage > 0)
        {
            m_CurrentHealth -= damage;
        }
        else Death();
    }
    public void Death()
    {
        if (t != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
        m_VisionCone.SetActive(false);
        m_Collider.enabled = false;
        m_Animator.enabled = false;
        m_NavMesh.enabled = false;
        es.dead = true;
    }
}
