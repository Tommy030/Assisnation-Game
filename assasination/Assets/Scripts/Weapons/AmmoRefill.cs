using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefill : MonoBehaviour
{
    private Gun m_ammoAmount;
    private int m_ammoRefillAmount;
    void Start()
    {
        //m_ammoAmount = GetComponent<Gun>();

        //m_ammoRefillAmount = m_ammoAmount.AmmoReserve;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Refill");
            //m_ammoAmount.AmmoRefill(m_ammoRefillAmount);
        }
    }
}
