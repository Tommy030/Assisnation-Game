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
    void Start()
    {
        Pistol.SetActive(false);
        Knife.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedWeapon = 2;
        }

        if (SelectedWeapon == 0)
        {
            Sniper.SetActive(false);
            Knife.SetActive(false);
            Pistol.SetActive(true);
            
        }
        else if (SelectedWeapon == 1)
        {
            Sniper.SetActive(false);
            Pistol.SetActive(false);
            Knife.SetActive(true);

        }
        else if (SelectedWeapon == 2)
        {
            Knife.SetActive(false);
            Pistol.SetActive(false);
            Sniper.SetActive(true);

        }
        
    }
}
