using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string reactor;
    public string returnItem;
    public string interactionFeedback;
    public string itemFeedback;
    public GameObject dialogBox;

    public GameObject newState;
    public GameObject player;

    void Start()
    {
        if (newState != null)
            newState.SetActive(false);
    }

    public void OnInteraction()
    {
        dialogBox.GetComponent<DialogBox>().SelectDialog(interactionFeedback);
        Debug.Log(interactionFeedback);
    }


/*
public void OnItemInteraction()
{
    if (newState != null)
    {
        newState.SetActive(true);
        gameObject.SetActive(false);
    }

    if (returnItem != null)
        player.GetComponent<Player>().recieveItem(returnItem);

    Debug.Log(itemFeedback);

    FindObjectOfType<SceneStateSaver>().SaveScene();

    if (newState != null && player != null)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.AddInteractable(newState);
            playerScript.RemoveInteractable(gameObject);
        }
    }

    ChangeScene changeScript = newState?.GetComponent<ChangeScene>();
    if (changeScript != null)
        changeScript.TriggerSceneChange();
}

*/

    public void OnItemInteraction()
    {
        if (newState != null)
        {
            newState.SetActive(true);
            gameObject.SetActive(false);
        }

        if (returnItem != null)
            player.GetComponent<Player>().recieveItem(returnItem);

        Debug.Log(itemFeedback);

        if (returnItem != null)
        {
            player.GetComponent<Player>().recieveItem(returnItem);
        }
        //Destroy(gameObject);
        dialogBox.GetComponent<DialogBox>().SelectDialog(itemFeedback);
        Debug.Log(itemFeedback);
        gameObject.SetActive(false);

        Object.FindFirstObjectByType<SceneStateSaver>().SaveScene();

        Player playerScript = player.GetComponent<Player>();

        // Força Unity a atualizar fisicamente o novo collider
        Physics2D.SyncTransforms();

        // Se o player está em cima do collider do newState, adiciona
        Collider2D c = newState?.GetComponent<Collider2D>();
        if (c != null && c.OverlapPoint(player.transform.position))
        {
            playerScript.AddInteractable(newState);
        }

        playerScript.RemoveInteractable(gameObject);

        ChangeScene changeScript = newState?.GetComponent<ChangeScene>();
        if (changeScript != null)
            changeScript.TriggerSceneChange();
    }
}
