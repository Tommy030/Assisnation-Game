using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermanager : MonoBehaviour
{
    public static Playermanager instance;
    public GameObject m_Player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
