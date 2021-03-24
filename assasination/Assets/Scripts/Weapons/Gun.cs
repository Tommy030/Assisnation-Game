using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Gun : MonoBehaviour
{
    public enum CurrentWeapon {Knife ,Pistol, Assault_rifle,  Sniper}
    [SerializeField] private CurrentWeapon cb;
    public GameObject m_WeaponPoint;
    public WeaponData m_weaponData;
    public GameObject FirePoint;

    //Time between shooting
    private float nextFire;

    [Header("weapon options")]
    public bool m_automatic;
    

    [Header("Muzzle Flash")]
    public bool muzzleflashOn;
    public ParticleSystem m_muzzleFlash;

    [Header("Audio")]
    public bool m_audioOn;
    

    [Header("Gizmo")]
    public float duration;


    private AudioSource m_audio;

    private bool m_useAmmo = true;
    private int m_clipsize;
    private int m_ammo;
    private bool m_reloading = false;
    private int ammo;

    [SerializeField] private float SoundRange = 10f;
    [SerializeField] private LayerMask m_EnemyLayer;

    private TMP_Text AmmoText;
    
    
    private int abc;
    private void Start()
    {
        ammo = m_weaponData.m_ammoAmount;
        m_audio = GetComponent<AudioSource>();
        m_WeaponPoint = GameObject.Find("weaponpoint");
        FirePoint = GameObject.Find("Main Camera");


        switch (cb)
        {
            case CurrentWeapon.Knife:
                {
                    m_useAmmo = false;
                    break;
                }
            case CurrentWeapon.Pistol:
                {
                    m_clipsize = 12;
                    break;
                }
            case CurrentWeapon.Assault_rifle:
                {
                    m_clipsize = 30;
                    break;
                }
            case CurrentWeapon.Sniper:
                {
                    m_clipsize = 1;
                    break;
                }
            default:
                {
                    Debug.Log("No Weapon Selected");
                    break; 
                }
        }
       
        m_ammo = m_clipsize;
    }
    void Update()
    {
        if (Time.timeScale == 1)
        {
            AmmoText = GameObject.Find("Ammotext").GetComponent<TMP_Text>();
        }
        gameObject.transform.position = m_WeaponPoint.transform.position;
        //gameObject.transform.rotation = m_WeaponPoint.transform.rotation;
        if(m_reloading == false)
        {
            if (m_automatic == false)
            {
                if (Input.GetMouseButtonDown(0) && Time.time > nextFire && m_ammo > 0)
                {
                    nextFire = Time.time + m_weaponData.m_fireRate;
                    Shoot();
                }
            }
            else if(Input.GetMouseButton(0) && Time.time > nextFire && m_ammo > 0)
            {
                nextFire = Time.time + m_weaponData.m_fireRate;
                Shoot();
            }

            if (m_ammo <= 0 && ammo != 0)
            {
                m_reloading = true;

                StartCoroutine(Reloading());
                StartCoroutine(Reload());

            }
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reload());
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
        if (m_useAmmo)
        {
            m_ammo--;
        }
        
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
            //Debug.Log(hit.collider.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            Window window = hit.transform.GetComponent<Window>();
            Sus sus = hit.transform.GetComponent<Sus>();
            if (window != null)
            {
                window.BreakWindow();
            }
            if(target != null)
            {
                //Debug.Log("Hit " + m_weaponData.m_damage);
                target.RemoveHP(m_weaponData.m_damage, hit.collider);
            }
            if (sus != null)
            {
                sus.DunDun();
            }

        }

    }
    
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(m_weaponData.m_reloadTime);
        abc = 0;
        abc = (m_ammo -= m_clipsize);

        if (ammo >= m_clipsize)
        {
            ammo -= -abc;
            m_ammo = m_clipsize;
        }
        else
        {
            m_ammo = ammo;
            ammo = 0;
        }

        AmmoText.text = m_ammo + "/" + ammo;
        Debug.Log("current ammo " + m_ammo);
        Debug.Log("current ammo reserve " + ammo);

        m_reloading = false;
        
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(m_weaponData.m_reloadTime);
        //12 
        if (ammo >= 0)
        {
            int AmountNeeded = m_clipsize - m_ammo;
            if (ammo >= AmountNeeded)
            {
                ammo -= AmountNeeded;
                m_ammo = m_clipsize;
            }
            else if( ammo> 0)
            {
                m_ammo += ammo;
            }
        }

        Debug.Log("current ammo " + m_ammo);
        Debug.Log("current ammo reserve " + ammo);

    }
}
