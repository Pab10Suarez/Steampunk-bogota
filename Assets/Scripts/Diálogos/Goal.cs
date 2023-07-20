using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private Dialogue dialogue;

    public void StartMission()
    {
        goal.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goal.SetActive(false);
            dialogue.Carry();
        }
    }

}
