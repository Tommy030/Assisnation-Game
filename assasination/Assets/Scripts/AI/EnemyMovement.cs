using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [Header("Navmesh")]
    public NavMeshHit Navhit;
    public NavMeshPath m_navmeshpath;

    private NavMeshAgent m_navmeshagent;
    [SerializeField] public ScriptableEnemy m_Enemy;
    private ScriptableEnemy m_PublicEnemy { set; get; }
    private bool m_Alert;

    private bool m_Wandering;
    private bool m_newwait;
    private Vector3 m_WanderLocation;
    private LayerMask Layer;
    [SerializeField] public float Angle = 10f;
    [SerializeField] public float Radius;
    [SerializeField] public bool m_SpottedTarget = false;
    private void Awake()
    {
        m_PublicEnemy = Instantiate(m_Enemy);

        m_navmeshagent = gameObject.AddComponent<NavMeshAgent>();
        m_navmeshagent.speed = m_PublicEnemy.m_Speed;
        m_navmeshagent.acceleration = m_PublicEnemy.m_AccelerationSpeed;

        gameObject.name = m_PublicEnemy.name;
    }
    public void AlertUnit(Vector3 targetpos)
    {
        if (!m_SpottedTarget)
        {

            if (m_navmeshagent.CalculatePath(targetpos, m_navmeshpath))
            {
                m_navmeshagent.SetPath(m_navmeshpath);
                m_Alert = true;
                m_Wandering = false;
            }
        }
    }
    private void Update()
    {
        if (m_Alert)
        {
            if (Vector3.Distance(gameObject.transform.position, m_navmeshagent.destination) < 0.3)
            {
                if (!m_SpottedTarget)
                {
                    Wander();
                }
            }
        }
        // else if (Vector3.Distance(Player.transform, transform.position)<= Radius)
        // {
        //   bool InFOXLOC = InFOV(transform, Player, Angle, Radius);
        // }
        else
        {
            if (!m_newwait)
            {
                Wander();
            }
        }
    }

    private void Wander()
    {
        if (!m_Wandering)
        {
            bool Haspath = false;
            while (!Haspath && !m_Alert && !m_SpottedTarget)
            {
                Haspath = RandomWanderTarget(transform.position, m_PublicEnemy.m_WanderRange, out m_WanderLocation);
            }
            if (Haspath && !m_Alert &!m_SpottedTarget)
            {
                m_navmeshagent.SetDestination(m_WanderLocation);
                m_Wandering = true;
            }
        }
        if (m_navmeshagent.pathStatus == NavMeshPathStatus.PathComplete && m_Wandering)
        {
            StartCoroutine(WaitHere(m_Wandering, m_PublicEnemy.m_DurationWaitingAtWanderingLocation));
        }
    }
    IEnumerator WaitHere(bool Change, float duration)
    {
        m_newwait = true;
        m_Wandering = false;
        yield return new WaitForSeconds(duration);
        Wander();
        m_newwait = false;
    }
    private bool RandomWanderTarget(Vector3 Centre, float range, out Vector3 Result)
    {
        Vector3 Randompoint = Centre + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(Randompoint, out Navhit, 1.0f, NavMesh.AllAreas))
        {
            Result = Navhit.position;
            return true;
        }
        else
        {
            Result = Centre;
            return false;
        }
    }

    public bool InFOV(Transform Check, Transform Target, float MaxAngle, float Radius)
    {
        Collider[] Overlaps = new Collider[300];
        int count = Physics.OverlapSphereNonAlloc(Check.position, Radius, Overlaps, Layer);
        for (int i = 0; i < count + 1; i++)
        {
            if (Overlaps[i] != null)
            {
                if (Overlaps[i].transform == Target)
                {
                    Vector3 directionbetween = (Target.position - Check.position).normalized;
                    directionbetween.y = 0;
                    float angle = Vector3.Angle(Check.forward, directionbetween);
                    if (angle <= MaxAngle)
                    {
                        Ray ray = new Ray(Check.position, Target.position - Check.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, Radius, Layer))
                        {

                            if (hit.transform == Target)
                            {

                                return true;
                            }
                        }
                    }
                    else
                        return false;
                }
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Vector3 FovLine1 = Quaternion.AngleAxis(Angle, transform.up) * transform.forward * Radius;
        Vector3 FovLine2 = Quaternion.AngleAxis(-Angle, transform.up) * transform.forward * Radius;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, FovLine1);
        Gizmos.DrawRay(transform.position, FovLine2);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * Radius);
    }

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

    }
}