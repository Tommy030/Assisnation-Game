using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private EnemyControl ec;
    void Start()
    {
        ec = gameObject.GetComponent<EnemyControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ec.m_CloseToPlayer == true)
        {
            ec.m_Animator.SetBool("ReachedTarget", true);
        }
        else
        {
            ec.m_Animator.SetBool("ReachedTarget", false);
        }
    }
}
