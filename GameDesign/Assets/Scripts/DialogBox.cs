using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public TextAsset dialogFile;

    private const string testControlerTag = "TestManager";

    private GameObject testCotroller;

    private DateTime dialogStart;

    private class DialogText
    {
        public string objectname;
        public string[] texts;

        public DialogText(string objectname, string rawText) {
            string continuation = rawText.Substring(rawText.IndexOf(";") + 1);
            this.objectname = objectname;
            texts = continuation.Split(";");
        }

    }

    private int tick;

    private List<DialogText> dialogText;
    private DialogText chosenDialog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string rawDialog = dialogFile.text;
        string[] allDialogs = rawDialog.Split("***");
        testCotroller = GameObject.Find(testControlerTag);
        dialogText = new List<DialogText>();
        tick = 0;
        foreach (string dialog in allDialogs)
        {
            Debug.Log(dialog);
            dialogText.Add(new DialogText(dialog.Split(';')[0], dialog));
        }
        foreach (string dialog in dialogText[0].texts)
            Debug.Log(dialog);
        chosenDialog = dialogText[0];
        gameObject.GetComponent<TextMeshProUGUI>().text = dialogText[0].texts[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tick > -1)
        {
            tick++;
            TimeSpan lastDialog = DateTime.Now - dialogStart;
            if(lastDialog < TimeSpan.FromMilliseconds(10000)) testCotroller.GetComponent<GQMTestController>().newDialog(lastDialog);
            if (tick < chosenDialog.texts.Length) {
                gameObject.GetComponent<TextMeshProUGUI>().text = chosenDialog.texts[tick];
            } else {
                tick = -1;
                gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    //public void SetText(string text)
    //{
    //    gameObject.GetComponent<TextMeshProUGUI>().text = text;
    //}

    public void SelectDialog(string objectForm)
    {
        //string allDialog = dialogFile.text;
        foreach (DialogText individualText in dialogText)
        {
            if(individualText.objectname == objectForm)
            {
                chosenDialog = individualText;
                gameObject.GetComponent<TextMeshProUGUI>().text = chosenDialog.texts[0];
                tick = 0;
                dialogStart = DateTime.Now;
            }
        }
    }

}
