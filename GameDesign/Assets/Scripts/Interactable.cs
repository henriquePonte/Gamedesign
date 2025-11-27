using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string reactor;
    public string returnItem;
    public string interactionFeedback;
    public string itemFeedback;

    public GameObject newState;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(newState != null) newState.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void OnInteraction()
    {
        Debug.Log(interactionFeedback);
    }

    public void OnItemInteraction()
    {
        newState.SetActive(true);
        if (returnItem != null) {
            player.GetComponent<Player>().recieveItem(returnItem);
        }
        //Destroy(gameObject);
        Debug.Log(itemFeedback);
        gameObject.SetActive(false);
    }
}
