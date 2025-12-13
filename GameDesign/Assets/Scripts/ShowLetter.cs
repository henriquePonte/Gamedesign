using UnityEngine;
using TMPro;

public class ShowLetter : MonoBehaviour
{
    public GameObject letterUI;
    public TMP_Text letterText;    
    [TextArea(3,10)]
    public string textToShow;        
    private bool hasShown = false;   

    void Start()
    {
        if(letterUI != null)
            letterUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasShown)
        {
            if(letterUI != null)
                letterUI.SetActive(true);

            if(letterText != null)
                letterText.text = textToShow;

            hasShown = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Opcional: você pode manter a carta aberta ou fechar quando sair
        // Se quiser que feche ao sair, descomente a linha abaixo:
        // if(other.CompareTag("Player")) letterUI.SetActive(false);
    }
}
