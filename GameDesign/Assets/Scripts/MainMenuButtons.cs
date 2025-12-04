using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour 
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Casa"); // nome da primeira cena
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
