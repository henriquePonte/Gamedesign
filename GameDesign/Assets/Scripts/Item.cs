using UnityEngine;

public class Key : MonoBehaviour
{
    public string itemName;

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void giveItem()
    {
        player.GetComponent<Player>().recieveItem(itemName);
        Destroy(gameObject);
    }
}
