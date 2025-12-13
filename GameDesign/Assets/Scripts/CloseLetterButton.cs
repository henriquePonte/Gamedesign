using UnityEngine;

public class CloseLetterButton : MonoBehaviour
{
    public GameObject letterUI; 

    public void CloseLetter()
    {
    Debug.Log("Botão da carta clicado");
        if(letterUI != null)
            letterUI.SetActive(false);

        Debug.Log("Botão da carta clicado: carta fechada!");
    }
}
