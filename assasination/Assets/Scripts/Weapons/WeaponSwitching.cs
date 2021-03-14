using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int SelectedWeapon;

    [Header("Weapons")]
    public GameObject Pistol;
    public GameObject Knife;
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

        if (SelectedWeapon == 0)
        {
            Knife.SetActive(false);
            Pistol.SetActive(true);
            
        }
        else
        {
            Pistol.SetActive(false);
            Knife.SetActive(true);
           
        }
    }
}
