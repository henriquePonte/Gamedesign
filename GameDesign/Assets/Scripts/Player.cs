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

    // Animation Variables
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string DIRECTION_ANIMATION = "Direction";


    void Start()
    {
        inventory = new List<string>(GameManager.instance.playerInventory);
        interactables = new List<GameObject>();
        TestCotroller = GameObject.Find(testControlerTag);
        still = true;
        stillStart = DateTime.Now;

        //Animation Declaration
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        AnimatePlayer();
        //InteractSurroundings();
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(movementX, movementY, 0f) * Time.deltaTime * speed;

        transform.position += movement;
        if (movement == Vector3.zero && !still)
        {
            still = true;
            stillStart = DateTime.Now;
        } else if (movement != Vector3.zero && still)
        {
            still = false;
            TestCotroller.GetComponent<GQMTestController>().stillUpdate(DateTime.Now - stillStart);
        }
    }

    // Animation Function
    void AnimatePlayer()
    {
        if (!still) {
            anim.SetBool(WALK_ANIMATION, true);
            if (movementY != 0) {
                anim.SetInteger(DIRECTION_ANIMATION, (int) movementY+2);
            } else {
                anim.SetInteger(DIRECTION_ANIMATION, (int)movementX + 1);
            }
        } else {
            anim.SetBool(WALK_ANIMATION, false);
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
}
