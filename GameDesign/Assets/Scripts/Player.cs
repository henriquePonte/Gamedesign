using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed;
    public GameObject memoryWarning;
    public KeyCode interactKey;

    private float movementX, movementY;
    private string memoryGateTag = "MemoryGate";
    private string memoryLocation = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CheckSurrondings();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        //Debug.Log("move x value is: " + movementX);
        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * speed;
    }

    void CheckSurrondings()
    {
        // Check if there is contact
        if (Input.GetKeyDown(interactKey) && memoryLocation != "")
        {
            SceneManager.LoadScene(memoryLocation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(memoryGateTag))
        {
            Debug.Log("Touching");
            memoryWarning.SetActive(true);
            memoryLocation = collision.gameObject.GetComponent<ChangeScene>().sceneName;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(memoryGateTag))
        {
            memoryWarning.SetActive(false);
            memoryLocation = "";
        }
    }
}
