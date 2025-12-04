using UnityEngine;

public class Key : MonoBehaviour
{
    public string itemName;
    public GameObject player;
    public GameObject dialogBox;

    public void giveItem()
    {
        // Dá o item ao player
        dialogBox.GetComponent<DialogBox>().SelectDialog(itemName);
        player.GetComponent<Player>().recieveItem(itemName);

        // Marca o item como coletado no GameManager
        GameManager.instance.CollectItem(itemName);

        Debug.Log($"Key coletada: {itemName}");

        // Desativa ou destrói o objeto
        gameObject.SetActive(false);
    }

    // Opcional: se você quiser que o item seja restaurado quando a cena é carregada pela primeira vez
    private void Start()
    {
        if (GameManager.instance.IsItemCollected(itemName))
        {
            // Item já foi coletado antes, não mostrar na cena
            gameObject.SetActive(false);
        }
    }
}
