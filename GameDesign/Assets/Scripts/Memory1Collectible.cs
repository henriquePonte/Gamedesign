using UnityEngine;

public class Memory1Collectible : MonoBehaviour
{
    [SerializeField] private int pieceID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Memory1Manager.Instance.CollectPiece(pieceID);
        Destroy(gameObject);
    }
}
