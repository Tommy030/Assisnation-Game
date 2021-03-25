using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefill : MonoBehaviour
{
    private int abc;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("Refill");
            Gun gunSript = other.GetComponent<Gun>();
            abc = gunSript.m_weaponData.m_ammoAmount;
            gunSript.AmmoRefill(abc);
            
        }
    }
}
