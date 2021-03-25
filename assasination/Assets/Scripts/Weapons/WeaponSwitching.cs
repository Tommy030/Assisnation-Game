using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int SelectedWeapon;

    [Header("Weapons")]
    public GameObject Pistol;
    public GameObject Knife;
    public GameObject Sniper;
    public GameObject Rifle;
    public GameObject S_Pistol;
    public GameObject S_Sniper;


    [SerializeField] private List<GameObject> SelectedWeapons = new List<GameObject>();
    [SerializeField] private GameObject Scope;
    void Start()
    {
        Pistol.SetActive(false);
        Knife.SetActive(false);
        GameObject obj = Instantiate(Knife);
        SelectedWeapons.Add(obj);
        GameObject obj1 = Instantiate(Pistol);
        SelectedWeapons.Add(obj1);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            SelectedWeapon = 0;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            SelectedWeapon = 1;
        }

        if (SelectedWeapon == 0)
        {
            
            SelectedWeapons[0].SetActive(true);
            SelectedWeapons[SelectedWeapons.Count - 1].GetComponent<GunStatus>().DisableGun();
        }
        else if (SelectedWeapon == 1)
        {
            SelectedWeapons[0].SetActive(false);
            SelectedWeapons[SelectedWeapons.Count - 1].GetComponent<GunStatus>().EnableGun();
        }

    }

    public void SelectPistol()
    {
        SelectedWeapons[SelectedWeapons.Count - 1].SetActive(false);
        GameObject obj = Instantiate(Pistol);
        SelectedWeapons.Add(obj);
        Time.timeScale = 1;

    }
    public void SelectSPistol()
    {
        SelectedWeapons[SelectedWeapons.Count - 1].SetActive(false);
        GameObject obj = Instantiate(S_Pistol);
        SelectedWeapons.Add(obj);
        Time.timeScale = 1;

    }
    public void SelectRifle()
    {
        SelectedWeapons[SelectedWeapons.Count - 1].SetActive(false);
        GameObject obj = Instantiate(Rifle);
        SelectedWeapons.Add(obj);
        Time.timeScale = 1;

    }
    public void SelectSniper()
    {
        SelectedWeapons[SelectedWeapons.Count - 1].SetActive(false);
        GameObject obj = Instantiate(Sniper);
        obj.GetComponent<Scope>().sniperScope = Scope;
        Time.timeScale = 1;
        SelectedWeapons.Add(obj);
    }
    public void SelectSSniper()
    {
        SelectedWeapons[SelectedWeapons.Count - 1].SetActive(false);
        GameObject obj = Instantiate(S_Sniper);
        SelectedWeapons.Add(obj);
        Time.timeScale = 1;

    }
}
