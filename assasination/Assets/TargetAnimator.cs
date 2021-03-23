using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimator : MonoBehaviour
{
    private EnemyControl ec;
    void Start()
    {
        ec = gameObject.GetComponent<EnemyControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ec.m_PlayerDetected == true)
        {
            ec.m_Animator.SetBool("running", true);
        }

        if (ec.m_CloseToPlayer == true)
        {
            ec.m_Animator.SetBool("attack", true);
        }
        else
        {
            ec.m_Animator.SetBool("attack", false);
        }
    }
}
