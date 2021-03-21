using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject checkPoint1;
    [SerializeField] private GameObject checkPoint2;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform BackUpTarget;

    [SerializeField] private NavMeshAgent NavMesh;

    private Vector3 CurrentDes;

    [SerializeField] private enemyState es;
    [SerializeField] private bool PatrolEnemy = false;

    private NavMeshPath NavMeshPath;

    [SerializeField] private bool m_PlayerDetected;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_RunSpeed = 16f;
    [SerializeField] private float m_WalkSpeed = 4f;
    [SerializeField] private float m_NavMeshStopDis = 5f;
 
    private void Start()
    {
        NavMeshPath = new NavMeshPath();
        Check1();
        player = GameObject.Find("player");
    }
    private void Update()
    {

        if (m_PlayerDetected == true)
        {
            chasingPlayer();
            NavMesh.speed = m_RunSpeed;
            Debug.Log(NavMesh.isStopped);
        }
        else
        {
            NavMesh.speed = m_WalkSpeed;
        }
     
        if (PatrolEnemy == true)
        {
            if (Vector3.Distance(gameObject.transform.position,checkPoint1.transform.position) < 3)
            {
                //hitCheckPoint1 = true;
                Check1();
            }
            if (Vector3.Distance(gameObject.transform.position, checkPoint2.transform.position) < 3)
            {
                //hitCheckPoint2 = true;
                Check2();
            }


            //if (hitCheckPoint1 == true)
            //{
            //    Check1();
            //}
            //else if (hitCheckPoint2 == true)
            //{
            //    Check2();
            //}
            if (Vector3.Distance(transform.position, CurrentDes) < 1 || NavMesh.isStopped == true)
            {
                m_Animator.SetBool("ReachedTarget", true);
                Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(lookAt);
            }
            else
            {
                m_Animator.SetBool("ReachedTarget", false);
            }
        }


        if (es.hunting == true)
        {
            chasingPlayer();
        
        }
    }
    private void Check1()
    {
        if (checkPoint2 != null)
        {
            Vector3 targetVector = checkPoint2.transform.position;
            NavMesh.SetDestination(targetVector);
          
        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten");
        }
    }

    private void Check2()
    {
        if (checkPoint1 != null)
        {
            Vector3 targetVector = checkPoint1.transform.position;
            NavMesh.SetDestination(targetVector);
        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten");
        }
    }

    private void chasingPlayer()
    {
        if (player != null)
        {
            Vector3 targetVector = player.transform.position;
            NavMesh.CalculatePath(targetVector, NavMeshPath);
            if (NavMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                NavMesh.SetDestination(targetVector);
                CurrentDes = player.transform.position;
                if (Vector3.Distance(transform.position,targetVector) < m_NavMeshStopDis)
                {
                    NavMesh.isStopped = true;
                }
                else
                {
                    NavMesh.isStopped = false;
                }
            }
            else
            {
                NavMesh.SetDestination(BackUpTarget.position);
                CurrentDes = BackUpTarget.position;
            }
            m_PlayerDetected = true;
        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten");
        }
    }
}
