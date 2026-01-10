using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed;
    public GameObject InteractionWarning;
    public KeyCode interactKey;

    private float movementX, movementY;

    [Header("Footsteps")]
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.4f;

private float stepTimer;

    private const string memoryGateTag = "MemoryGate";
    private const string itemTag = "Item";
    private const string interactableTag = "Interactable";
    private const string teleportTag = "Teleport";
    private const string testControlerTag = "TestManager";

    private List<string> inventory;
    private List<GameObject> interactables;

    private bool still;

    private DateTime stillStart;

    private GameObject TestCotroller;


    void Start()
    {
        inventory = new List<string>(GameManager.instance.playerInventory);
        interactables = new List<GameObject>();
        TestCotroller = GameObject.Find(testControlerTag);
        still = true;
        stillStart = DateTime.Now;
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

        Vector3 movement = new Vector3(movementX, movementY, 0f).normalized 
                        * Time.deltaTime * speed;

        transform.position += movement;

        bool isMoving = movement != Vector3.zero;

        HandleFootsteps(isMoving);

        if (!isMoving && !still)
        {
            still = true;
            stillStart = DateTime.Now;
        }
        else if (isMoving && still)
        {
            still = false;
            TestCotroller.GetComponent<GQMTestController>()
                .stillUpdate(DateTime.Now - stillStart);
        }
    }

    void InteractSurroundings()
    {
        if (Input.GetKeyDown(interactKey) && interactables.Count > 0)
        {
            GameObject firstInteractible = interactables[0];

            switch (firstInteractible.tag)
            {
                case itemTag:
                    TestCotroller.GetComponent<GQMTestController>().addInteraction(true);
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
                        TestCotroller.GetComponent<GQMTestController>().addInteraction(true);
                        firstInteractible.GetComponent<Interactable>().OnItemInteraction();
                        interactables.Remove(firstInteractible); 
                    }
                    else
                    {
                        TestCotroller.GetComponent<GQMTestController>().addInteraction(false);
                        firstInteractible.GetComponent<Interactable>().OnInteraction();
                    }
                    break;
                case teleportTag:
                    TeleportArea tp = firstInteractible.GetComponent<TeleportArea>();
                    if (tp != null)
                    {
                        tp.TeleportPlayer(gameObject); 
                        interactables.Remove(firstInteractible);
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

        if (collisionTag == memoryGateTag || collisionTag == itemTag || 
            collisionTag == interactableTag || collisionTag == teleportTag)
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

        if (collisionTag == memoryGateTag || collisionTag == itemTag || 
            collisionTag == interactableTag || collisionTag == teleportTag)
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

    void HandleFootsteps(bool isMoving)
{
    if (!isMoving)
    {
        stepTimer = 0f;
        return;
    }

    stepTimer += Time.deltaTime;

    if (stepTimer >= stepInterval)
    {
        PlayFootstep();
        stepTimer = 0f;
    }
}

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        footstepSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        AudioClip clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip);
    }

}
