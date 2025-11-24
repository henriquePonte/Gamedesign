using UnityEngine;
using UnityEngine.SceneManagement; 

public class ChangeScene : MonoBehaviour
{
    // Nome da cena que vai ser carregada
    public string sceneName;

    private void OnMouseDown()
    {
        // Quando o sprite for tocado troca a cena
        SceneManager.LoadScene(sceneName);
    }
}
