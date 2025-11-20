using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;

    private float movementX, movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        //Debug.Log("move x value is: " + movementX);
        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * speed;
    }
}
