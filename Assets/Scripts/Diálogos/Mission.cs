using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] GameObject chest;
    [SerializeField] private bool chestCarried;
    [SerializeField] private Dialogue dialogue;

    void Start()
    {
        chestCarried = false;
    }

    public void StartMission()
    {
        chest.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            chest.SetActive(false);
            chestCarried = true;
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            if (chestCarried)
            {
                dialogue.MissionFinished();
            }
        }
    }
}
