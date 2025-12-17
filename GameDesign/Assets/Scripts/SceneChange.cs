using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    private const string testControlerTag = "TestManager";

    public void TriggerSceneChange()
    {
        SceneStateSaver saver = FindFirstObjectByType<SceneStateSaver>();
        if (saver != null)
            saver.SaveScene();

        if (!string.IsNullOrEmpty(sceneName))
        {
            GameObject.Find(testControlerTag).GetComponent<GQMTestController>().enteringLocation(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
