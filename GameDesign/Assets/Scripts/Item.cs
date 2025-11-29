using UnityEngine;

public class Key : MonoBehaviour
{
    public string itemName;
    public GameObject player;

    public void giveItem()
    {
        // Dá o item ao player
        player.GetComponent<Player>().recieveItem(itemName);

        // Marca o item como coletado no GameManager
        GameManager.instance.CollectItem(itemName);

        // Desativa ou destrói o objeto
        gameObject.SetActive(false);

        Debug.Log($"Key coletada: {itemName}");
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
