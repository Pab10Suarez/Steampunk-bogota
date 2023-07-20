using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject noButton, siButton;
    [SerializeField] private Goal goal;
    [SerializeField] private string finishString;

    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private bool saidNo;
    private bool chestCarried;
    private bool missionFinished;
    private bool inMission;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !inMission)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }

        if (lineIndex == dialogueLines.Length && Input.GetKeyDown(KeyCode.E) && !inMission)
        {
            siButton.SetActive(true);
            noButton.SetActive(true);
        }

        if (saidNo || missionFinished)
        {
            float moveDistance = 5f * Time.deltaTime;
            transform.Translate(Vector3.right * moveDistance);
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !inMission)
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (chestCarried)
            {
                StartCoroutine(ShowFinishLine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    public void SayNo()
    {
        saidNo = true;
        siButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void SayYes()
    {
        goal.StartMission();
        inMission = true;
        siButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void MissionFinished()
    {
        StartCoroutine(ShowFinishLine());
    }

    private IEnumerator ShowFinishLine()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;

        dialogueText.text = string.Empty;

        foreach (char ch in finishString)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        missionFinished = true;
    }

    public void Carry()
    {
        chestCarried = true;
    }
}
