using UnityEngine;

public class Memory1Collectible : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {        
            Memory1Manager.Instance.CollectPiece();
            Destroy(gameObject);
        }
    }
}
