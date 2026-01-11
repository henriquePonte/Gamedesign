using UnityEngine;
using System.Collections.Generic;

public class Memory1Manager : MonoBehaviour
{
    public static Memory1Manager Instance;
    public Memory1Dialogue dialogue;

    public int totalPieces;
    public int collectedPieces = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectPiece()
    {
        collectedPieces++;
        Debug.Log($"Piece collected: {collectedPieces}/{totalPieces}");

        if (collectedPieces >= totalPieces)
        {
           MemoryCompleted();
        }
    }

    void MemoryCompleted()
    {
        Debug.Log("Memory 1 complete");
        dialogue.StartDialogue();
        // here: next memory, sound, etc
    }
}
