using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFieldOfView : MonoBehaviour
{
    [SerializeField] private float viewRadius;
    [Range(0,360)]
    [SerializeField] private float viewAngle;

    [SerializeField] private LayerMask targetlayerMask;
    [SerializeField] private LayerMask obstakelLayerMask;

    //cone
    [SerializeField] private float meshResolution;

    [SerializeField] private MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    [SerializeField] private int edgeResolveItter;
    [SerializeField] private float edgeDistTreshhold;
    [SerializeField] private enemyState es;
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "viewMesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("Findtargetwithdelay", .2f);
    }
    IEnumerator Findtargetwithdelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            findVisibleTargets();
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }
    private void findVisibleTargets()
    {
        Collider[] targetsInviewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetlayerMask);

        for (int i = 0; i < targetsInviewRadius.Length; i++)
        {
            Transform target = targetsInviewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward,dirToTarget) < viewAngle/2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position,dirToTarget,distanceToTarget,obstakelLayerMask))
                {
                    es.hunting = true;
                   
                }
                else
                {
                    es.hunting = false;
                   
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (angleIsGlobal == false)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
            ViewCastInfo newViewCast = viewCast(angle);

            if (i > 0)
            {
                bool edgeDistTreshExceed = Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistTreshhold;
                if (oldViewCast.hit != newViewCast.hit)
                {
                    edgeInfo edge = findEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero ||(oldViewCast.hit && newViewCast.hit && edgeDistTreshExceed))
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }
            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint( viewPoints[i]);
            
            if (i < vertexCount -2 )
            {              
                triangles[i * 3] = 0;           
                triangles[i * 3 + 1] = i +1;           
                triangles[i * 3 + 2] = i +2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    edgeInfo findEdge(ViewCastInfo minvc, ViewCastInfo maxvc)
    {
        float minangle = minvc.angle;
        float maxangle = maxvc.angle;
        Vector3 minpoint = Vector3.zero;
        Vector3 maxpoint = Vector3.zero;

        for (int i = 0; i < edgeResolveItter; i++)
        {
            float angle = (minangle + maxangle) / 2;
            ViewCastInfo newViewCast = viewCast (angle);

            bool edgeDistTreshExceed = Mathf.Abs(minvc.distance - newViewCast.distance) > edgeDistTreshhold;
            if (newViewCast.hit == minvc.hit && !edgeDistTreshExceed)
            {
                minangle = angle;
                minpoint = newViewCast.point;
            }
            else
            {
                maxangle = angle;
                maxpoint = newViewCast.point;
            }
        }
        return new edgeInfo(minpoint, maxpoint);
    }
    ViewCastInfo viewCast(float gloabalAngle)
    {
        Vector3 dir = DirFromAngle(gloabalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position,dir, out hit, viewRadius,obstakelLayerMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, gloabalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius,viewRadius, gloabalAngle);
        }
    }
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    public struct edgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public edgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
