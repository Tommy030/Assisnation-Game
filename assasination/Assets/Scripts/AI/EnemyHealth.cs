using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public ScriptableEnemy m_PublicEnemy;

    private Target t;
    private enemyState es;

    private float m_CurrentHealth;
    private void Start()
    {
        t = gameObject.GetComponent<Target>();
        es = gameObject.GetComponent<enemyState>();
        m_CurrentHealth = m_PublicEnemy.Health;
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
        gameObject.SetActive(false);
    }
}
