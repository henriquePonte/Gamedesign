using UnityEngine;

public class Camera_Following : MonoBehaviour
{
    private Transform player;

    private Vector3 tempPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;

        transform.position = tempPos;
    }
}
