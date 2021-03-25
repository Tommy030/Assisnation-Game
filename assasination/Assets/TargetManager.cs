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
                Tip.text = "assassinate the target, he hasn't been spotted al day find him"; break;
            case 1: Targets[random].SetActive(true);
                Tip.text = "assassinate the target, hasn't left his home"; break;
            case 2: Targets[random].SetActive(true);
                Tip.text = "assassinate the target, he has been moving between te buildings al day"; break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
