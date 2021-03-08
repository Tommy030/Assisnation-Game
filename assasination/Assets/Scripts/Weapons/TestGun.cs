using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public WeaponData m_weaponData;
    public GameObject FirePoint;
    
    //Time between shooting
    private float nextFire;

    //For the Gizmo
    private float duration;

    [Header("Automatic weapon")]
    public bool m_automatic;

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
        
        //OnDrawGizmos();
        Debug.DrawRay(transform.position, FirePoint.transform.TransformDirection(Vector3.forward) * m_weaponData.m_shootRange, Color.white, duration = 0.3f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, FirePoint.transform.forward, out hit, m_weaponData.m_shootRange))
        {

            //hier in zet je wat het moet doen
            /*targethealth target = hit.transform.GetComponent<targethealth>();
            if(target != null)
            {
                target.TakeDamage(m_weaponData.m_damage);
            }*/

        }
        Debug.Log("Shoot");
    }
}
