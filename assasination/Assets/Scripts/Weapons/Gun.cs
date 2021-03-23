using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject m_WeaponPoint;
    public WeaponData m_weaponData;
    public GameObject FirePoint;

    //Time between shooting
    private float nextFire;

    [Header("Automatic weapon")]
    public bool m_automatic;

    [Header("Muzzle Flash")]
    public bool muzzleflashOn;
    public ParticleSystem m_muzzleFlash;

    [Header("Audio")]
    public bool m_audioOn;
    

    [Header("Gizmo")]
    public float duration;


    private AudioSource m_audio;


    [SerializeField] private float SoundRange = 10f;
    [SerializeField] private LayerMask m_EnemyLayer;
    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_WeaponPoint = GameObject.Find("weaponpoint");
        FirePoint = GameObject.Find("Main Camera");
    }
    void Update()
    {
        
        gameObject.transform.position = m_WeaponPoint.transform.position;
        //gameObject.transform.rotation = m_WeaponPoint.transform.rotation;
        if (m_automatic == false)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + m_weaponData.m_fireRate;
                Shoot();
            }
        }
        else
        {
            if(Input.GetMouseButton(0) && Time.time > nextFire)
            {
                nextFire = Time.time + m_weaponData.m_fireRate;
                Shoot();
            }
        }

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, SoundRange, m_EnemyLayer);

        if (enemiesInRange.Length != 0)
        {
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                enemiesInRange[i].GetComponent<enemyState>().hunting = true;
            }
        }
    }
    private void Shoot()
    {
        if(muzzleflashOn == true)
        {
            m_muzzleFlash.Play();
        }

        if(m_audioOn == true)
        {
            m_audio.Play();
        }
        

        //OnDrawGizmos();
        Debug.DrawRay(FirePoint.transform.position, FirePoint.transform.TransformDirection(Vector3.forward) * m_weaponData.m_shootRange, Color.blue, duration);
        RaycastHit hit;
        if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, m_weaponData.m_shootRange))
        {
            Debug.Log(hit.collider.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target != null)
            {
                //Debug.Log("Hit " + m_weaponData.m_damage);
                target.RemoveHP(m_weaponData.m_damage, hit.collider);
            }

        }

    }

}
