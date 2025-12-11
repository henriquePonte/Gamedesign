using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public Vector3 teleportPosition;

    public void TeleportPlayer(GameObject player)
    {
        player.transform.position = teleportPosition;
    }
}
