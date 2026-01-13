using UnityEngine;
using TMPro;
using System.Collections;

public class Memory1Dialogue : MonoBehaviour 
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [TextArea(3, 6)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool canAdvance = false;

    void Update()
    {
        if (!dialoguePanel.activeSelf /*|| !canAdvance*/) return;
        
        if (canAdvance && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void StartDialogue()
    {
        //Debug.Log("Dialogue lines length: " + dialogueLines.Length);

        dialoguePanel.SetActive(true);
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];

        Time.timeScale = 0f; // opcional
        StartCoroutine(EnableAdvanceNextFrame());
        //canAdvance = false;
        //Invoke(nameof(EnableAdvance), 0.15f);
    }

    IEnumerator EnableAdvanceNextFrame()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        canAdvance = true;
    }

    //void EnableAdvance()
    //{
    //    canAdvance = true;
    //}

    void NextLine()
    {
        canAdvance = false;
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine];
            StartCoroutine(EnableAdvanceNextFrame());
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
        canAdvance = false;

        Debug.Log("Memory 1 dialogue finished");
        //proxima memoria
    }
}
