using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Targets;
    [SerializeField] private TMP_Text Tip;
    void Start()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0: Targets[random].SetActive(true);
                Tip.text = "Assassinate the target, he hasn't been spotted all day. Find him"; break;
            case 1: Targets[random].SetActive(true);
                Tip.text = "Assassinate the target, he hasn't left his home"; break;
            case 2: Targets[random].SetActive(true);
                Tip.text = "Assassinate the target, he's been moving between the buildings all day"; break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
