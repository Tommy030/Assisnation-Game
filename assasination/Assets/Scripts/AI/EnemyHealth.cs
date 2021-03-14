using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ScriptableEnemy m_PublicEnemy;
    public void RemoveHP(int damage)
    {
        if (m_PublicEnemy.Health - damage > 0)
        {
            m_PublicEnemy.Health -= damage;
        }
        else Death();
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }
}
