using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject goal, dialogueChest;
    [SerializeField] private Dialogue dialogue;
    private bool readyToCatch;

    public void StartMission()
    {
        goal.SetActive(true);
        dialogueChest.SetActive(false);
        readyToCatch = false;
    }

    private void Update()
    {
        if (readyToCatch && Input.GetKeyDown(KeyCode.E))
        {
            goal.SetActive(false);
            dialogue.Carry();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            readyToCatch = true;
            dialogueChest.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueChest.SetActive(false);
            readyToCatch = false;
        }
    }
}
