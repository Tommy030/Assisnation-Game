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

    [SerializeField] private NavMeshAgent navmesh;


    private bool hitCheckPoint1;
    private bool hitCheckPoint2;

    [SerializeField] private enemyState es;
    [SerializeField] private bool PatrolEnemy = false;
    private void Start()
    {
        Check1();
        player = GameObject.Find("player");
        hitCheckPoint1 = false;
        chasingPlayer();

    }
    private void Update()
    {

        if (player == null)
        {
            Debug.Log("het werkt in hrt navmesh script");
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
        }


        if (es.hunting == true)
        {
            chasingPlayer();
            hitCheckPoint1 = false;
            hitCheckPoint2 = false;
        }
    }
    private void Check1()
    {
        if (checkPoint2 != null)
        {
            Vector3 targetVector = checkPoint2.transform.position;
            navmesh.SetDestination(targetVector);
            hitCheckPoint1 = false;
        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten n word");
        }
    }

    private void Check2()
    {
        if (checkPoint1 != null)
        {
            Vector3 targetVector = checkPoint1.transform.position;
            navmesh.SetDestination(targetVector);
            hitCheckPoint2 = false;
        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten n word");
        }
    }

    private void chasingPlayer()
    {
        if (player != null)
        {
            Vector3 targetVector = player.transform.position;
            navmesh.SetDestination(targetVector);

        }
        else
        {
            Debug.Log("je bent vergeten de transform erin te zetten n word");
        }
    }
}
