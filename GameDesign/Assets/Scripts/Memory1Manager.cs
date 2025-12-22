using UnityEngine;
using System.Collections.Generic;

public class Memory1Manager : MonoBehaviour
{
    public static Memory1Manager Instance;

    [SerializeField] private int totalPieces; //to be defined? both floors
    private HashSet<int> collectedPieces = new HashSet<int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectPiece(int pieceID)
    {
        if (collectedPieces.Contains(pieceID)) return;

        collectedPieces.Add(pieceID);

        Debug.Log($"Piece collected: {collectedPieces.Count}/{totalPieces}");

        if (collectedPieces.Count >= totalPieces)
        {
            MemoryCompleted();
        }
    }

    void MemoryCompleted()
    {
        Debug.Log("Memory 1 complete");
        // here: next memory, sound, etc
    }
}
