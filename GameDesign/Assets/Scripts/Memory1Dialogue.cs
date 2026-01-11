using UnityEngine;
using TMPro;

public class Memory1Dialogue : MonoBehaviour 
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [TextArea(3, 6)]
    public string[] dialogueLines;

    private int currentLine = 0;
    //private bool canAdvance = false;

    void Update()
    {
        if (!dialoguePanel.activeSelf /*|| !canAdvance*/) return;
        
        if (Input.GetKeyDown(KeyCode.E))
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
        //canAdvance = false;
        //Invoke(nameof(EnableAdvance), 0.1f);
    }

    //void EnableAdvance()
    //{
    //    canAdvance = true;
    //}

    void NextLine()
    {
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        Debug.Log("Memory 1 dialogue finished");
        //proxima memoria
    }
}
