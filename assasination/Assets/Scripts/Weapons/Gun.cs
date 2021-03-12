using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponData m_weaponData;
    public GameObject FirePoint;
    
    //Time between shooting
    private float nextFire;

    [Header("Automatic weapon")]
    public bool m_automatic;

    [Header("Gizmo")]
    public float duration;

    public ParticleSystem m_muzzleFlash;

    void Update()
    {
        if (m_automatic == false)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + m_weaponData.m_fireRate;
                Shoot();
            }
        }
        else if(m_automatic == true)
        {
            if(Input.GetMouseButton(0) && Time.time > nextFire)
            {
                nextFire = Time.time + m_weaponData.m_fireRate;
                Shoot();
            }
        }
        
    }
    private void Shoot()
    {
        m_muzzleFlash.Play();
        //OnDrawGizmos();
        Debug.DrawRay(FirePoint.transform.position, FirePoint.transform.TransformDirection(Vector3.forward) * m_weaponData.m_shootRange, Color.red, duration);
        RaycastHit hit;
        if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, m_weaponData.m_shootRange))
        {

            //hier in zet je wat het moet doen
            EnemyMovement target = hit.transform.GetComponent<EnemyMovement>();
            if(target != null)
            {
                Debug.Log("Shoot");
                target.RemoveHP(m_weaponData.m_damage);
            }

        }

    }
}
