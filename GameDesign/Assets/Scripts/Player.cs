using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed;
    public GameObject InteractionWarning;
    public KeyCode interactKey;

    private float movementX, movementY;

    //private string memoryLocation = "";
    private const string memoryGateTag = "MemoryGate";
    private const string itemTag = "Item";
    private const string interactableTag = "Interactable";

    private List<string> inventory;
    private List<GameObject> interactables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = new List<string>();
        interactables = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        InteractSurroundings();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        //Debug.Log("move x value is: " + movementX);
        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * speed;
    }

    void InteractSurroundings() {
        // Check if there is contact
        if (Input.GetKeyDown(interactKey) && interactables.Count > 0) {
            GameObject firstInteractible = interactables[0];
            interactables.RemoveAt(0);
            switch (firstInteractible.tag) {
                case itemTag:
                    firstInteractible.GetComponent<Key>().giveItem();
                    break;
                case memoryGateTag:
                    ChangeScene changeScript = firstInteractible.GetComponent<ChangeScene>();
                    if (changeScript != null)
                    {
                        changeScript.TriggerSceneChange();
                    }
                break;
                case interactableTag:
                    string neededItem = firstInteractible.GetComponent<Interactable>().reactor;
                    if (inventory.Contains(neededItem) && !neededItem.Equals("")){
                        inventory.Remove(neededItem);
                        firstInteractible.GetComponent<Interactable>().OnItemInteraction();
                    } else {
                        firstInteractible.GetComponent<Interactable>().OnInteraction();
                        interactables.Add(firstInteractible);
                    }
                    break;
            }
        }
    }

    public void recieveItem(string item)
    {
        Debug.Log(item);
        inventory.Add(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.tag;
        Debug.Log(collisionTag);
        if (collisionTag.Equals(memoryGateTag) || collisionTag.Equals(itemTag) || collisionTag.Equals(interactableTag)){
            if (interactables.Count == 0) InteractionWarning.SetActive(true);
            interactables.Add(collision.gameObject);
        }
        //switch (collision.tag){
        //    case memoryGateTag:
        //        memoryLocation = collision.gameObject.GetComponent<ChangeScene>().sceneName;
        //        break;
        //    case itemTag:
        //        InteractionWarning.SetActive(true);
        //        interactables.Add(collision.gameObject);
        //        break;
        //    case interactableTag:

        //        break;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string collisionTag = collision.tag;
        if (collisionTag.Equals(memoryGateTag) || collisionTag.Equals(itemTag) || collisionTag.Equals(interactableTag)){
            interactables.Remove(collision.gameObject);
            if (interactables.Count == 0) InteractionWarning.SetActive(false);
        }
    }
}
