using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<string, SceneSaveData> sceneStates = new Dictionary<string, SceneSaveData>();
    public HashSet<string> collectedItems = new HashSet<string>();

    public List<string> playerInventory = new List<string>();
    public int testId = 0;
    public string testFile;
    public string[] testParameters;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCurrentScene(SceneSaveData data)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneStates[sceneName] = data;
        Debug.Log($"GameManager: cena '{sceneName}' salva com {data.objects.Count} objetos.");
    }

    public SceneSaveData GetSceneState(string sceneName)
    {
        if (sceneStates.ContainsKey(sceneName))
            return sceneStates[sceneName];
        return null;
    }

    public void CollectItem(string itemName)
    {
        collectedItems.Add(itemName);
    }

    public bool IsItemCollected(string itemName)
    {
        return collectedItems.Contains(itemName);
    }
}
