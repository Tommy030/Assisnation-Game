using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    [SerializeField] public GameObject m_WeaponPoint;
    void Start()
    {
        m_WeaponPoint = GameObject.Find("weaponpoint");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = m_WeaponPoint.transform.rotation;
    }
}
