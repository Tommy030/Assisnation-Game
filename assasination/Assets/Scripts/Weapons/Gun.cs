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
    private GameObject FirePoint;

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
    private int CurrentAmmo;
    public bool m_reloading = false;
    public int AmmoReserve;

    [SerializeField] private float SoundRange = 10f;
    [SerializeField] private LayerMask m_EnemyLayer;



    public Vector3 upRecoil;
    Vector3 originalRot;
    

    private TMP_Text AmmoText;
    
    
    private int abc;

    public bool IsSelected;
    private void Start()
    {
        originalRot = transform.localEulerAngles;

        AmmoReserve = m_weaponData.m_ammoAmount;
        m_audio = GetComponent<AudioSource>();
        m_WeaponPoint = GameObject.Find("weaponpoint");
        FirePoint = GameObject.Find("Main Camera");


        switch (cb)
        {
            case CurrentWeapon.Knife:
                {
                    m_useAmmo = false;
                    IsSelected = true;
                    break;
                }
            case CurrentWeapon.Pistol:
                {
                    
                    m_clipsize = 12;
                    m_useAmmo = true;
                    break;
                }
            case CurrentWeapon.Assault_rifle:
                {
                    m_clipsize = 30;
                    m_useAmmo = true;
                    break;
                }
            case CurrentWeapon.Sniper:
                {
                    m_clipsize = 1;
                    m_useAmmo = true;
                    break;
                }
            default:
                {
                    Debug.Log("No Weapon Selected");
                    break; 
                }
        }
       
        CurrentAmmo = m_clipsize;
    }
    void Update()
    {
        if (IsSelected == true)
        {



            if (Time.timeScale == 1)
            {
                AmmoText = GameObject.Find("Ammotext").GetComponent<TMP_Text>();
                AmmoText.text = CurrentAmmo + "/" + AmmoReserve;
            }
            gameObject.transform.position = m_WeaponPoint.transform.position;
            //gameObject.transform.rotation = m_WeaponPoint.transform.rotation;
            if (m_reloading == false)
            {
                if (m_automatic == false)
                {
                    if (Input.GetMouseButtonDown(0) && Time.time > nextFire && CurrentAmmo > 0)
                    {
                        nextFire = Time.time + m_weaponData.m_fireRate;
                        Shoot();
                        AddRecoil();
                    }
                }
                else if (Input.GetMouseButton(0) && Time.time > nextFire && CurrentAmmo > 0)
                {
                    nextFire = Time.time + m_weaponData.m_fireRate;
                    Shoot();
                    AddRecoil();
                }
                if (m_useAmmo == true)
                {
                    if (Input.GetKey(KeyCode.R) || CurrentAmmo <= 0 && AmmoReserve != 0)
                    {
                        m_reloading = true;
                        StartCoroutine(Reload());

                    }
                }


                if (cb == CurrentWeapon.Knife)
                {
                    if (Input.GetMouseButton(0) && Time.time > nextFire)
                    {
                        nextFire = Time.time + m_weaponData.m_fireRate;
                        Shoot();
                        AddRecoil();
                    }
                }

            }

        }
        
    }
    private void Shoot()
    {

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, SoundRange, m_EnemyLayer);

        if (enemiesInRange.Length != 0)
        {
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                enemiesInRange[i].GetComponent<enemyState>().hunting = true;
            }
        }
        if (m_useAmmo)
        {
            CurrentAmmo--;
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
    

    public void Reloading()
    {
        if (AmmoReserve >= 0)
        {
            int AmountNeeded = m_clipsize - CurrentAmmo;
            if (AmmoReserve >= AmountNeeded)
            {
                AmmoReserve -= AmountNeeded;
                CurrentAmmo = m_clipsize;
            }
            else if (AmmoReserve > 0)
            {
                CurrentAmmo += AmmoReserve;
            }
        }

        Debug.Log("current ammo " + CurrentAmmo);
        Debug.Log("current ammo reserve " + AmmoReserve);
        m_reloading = false;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(m_weaponData.m_reloadTime);
        //12 
        if (AmmoReserve >= 0)
        {
            int AmountNeeded = m_clipsize - CurrentAmmo;
            if (AmmoReserve >= AmountNeeded)
            {
                AmmoReserve -= AmountNeeded;
                CurrentAmmo = m_clipsize;
            }
            else if( AmmoReserve> 0)
            {
                CurrentAmmo += AmmoReserve;
                AmmoReserve = 0;
            }
        }

        Debug.Log("current ammo " + CurrentAmmo);
        Debug.Log("current ammo reserve " + AmmoReserve);
        m_reloading = false;
    }

    private void AddRecoil()
    {
        transform.localEulerAngles += upRecoil;
        Invoke("StopRecoil", 0.1f);
        
    }
    private void StopRecoil()
    {
        transform.localEulerAngles = originalRot;
    }
    public void AmmoRefill(int ammorefill)
    {
        int abc = ammorefill;
        AmmoReserve = abc;
    }
}
