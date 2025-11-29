using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void TriggerSceneChange()
    {
        SceneStateSaver saver = FindFirstObjectByType<SceneStateSaver>();
        if (saver != null)
            saver.SaveScene();

        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
