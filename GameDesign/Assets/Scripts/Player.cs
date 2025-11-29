using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed;
    public GameObject InteractionWarning;
    public KeyCode interactKey;

    private float movementX, movementY;

    private const string memoryGateTag = "MemoryGate";
    private const string itemTag = "Item";
    private const string interactableTag = "Interactable";

    private List<string> inventory;
    private List<GameObject> interactables;

    void Start()
    {
        inventory = new List<string>(GameManager.instance.playerInventory);
        interactables = new List<GameObject>();
    }

    void Update()
    {
        PlayerMovement();
        InteractSurroundings();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * speed;
    }

    void InteractSurroundings()
    {
        if (Input.GetKeyDown(interactKey) && interactables.Count > 0)
        {
            GameObject firstInteractible = interactables[0]; 

            switch (firstInteractible.tag)
            {
                case itemTag:
                    firstInteractible.GetComponent<Key>().giveItem();
                    interactables.Remove(firstInteractible);
                    break;

                case memoryGateTag:
                    ChangeScene changeScript = firstInteractible.GetComponent<ChangeScene>();
                    if (changeScript != null)
                    {
                        changeScript.TriggerSceneChange();
                        interactables.Remove(firstInteractible);
                    }
                    break;

                case interactableTag:
                    string neededItem = firstInteractible.GetComponent<Interactable>().reactor;
                    if (inventory.Contains(neededItem) && !neededItem.Equals(""))
                    {
                        inventory.Remove(neededItem);
                        firstInteractible.GetComponent<Interactable>().OnItemInteraction();
                        interactables.Remove(firstInteractible); 
                    }
                    else
                    {
                        firstInteractible.GetComponent<Interactable>().OnInteraction();
                    }
                    break;
            }
        }
    }

public void recieveItem(string item)
{
    Debug.Log(item);
    inventory.Add(item);

    GameManager.instance.playerInventory = new List<string>(inventory);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.tag;
        if (collisionTag == memoryGateTag || collisionTag == itemTag || collisionTag == interactableTag)
        {
            if (interactables.Count == 0)
                InteractionWarning.SetActive(true);

            if (!interactables.Contains(collision.gameObject))
                interactables.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string collisionTag = collision.tag;
        if (collisionTag == memoryGateTag || collisionTag == itemTag || collisionTag == interactableTag)
        {
            interactables.Remove(collision.gameObject);
            if (interactables.Count == 0)
                InteractionWarning.SetActive(false);
        }
    }

    // para manipular a lista de interagíveis
    public void AddInteractable(GameObject obj)
    {
        if (!interactables.Contains(obj))
            interactables.Add(obj);
    }

    public void RemoveInteractable(GameObject obj)
    {
        interactables.Remove(obj);
    }
}
