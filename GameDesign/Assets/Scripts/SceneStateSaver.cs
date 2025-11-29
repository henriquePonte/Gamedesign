using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneStateSaver : MonoBehaviour
{
    public void SaveScene()
    {
        SceneSaveData data = new SceneSaveData();
        data.objects = new List<SceneObjectData>();

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var root in rootObjects)
            SaveObjectRecursive(root, data);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            data.playerPosition = player.transform.position;

        GameManager.instance.SaveCurrentScene(data);
    }

    private void SaveObjectRecursive(GameObject obj, SceneSaveData data)
    {
        var unique = obj.GetComponent<UniqueID>();
        string id = unique != null ? unique.id : obj.name;

        SceneObjectData objData = new SceneObjectData
        {
            objectName = id,
            position = obj.transform.position,
            rotation = obj.transform.rotation,
            scale = obj.transform.localScale,
            isActive = obj.activeSelf
        };

        data.objects.Add(objData);

        foreach (Transform child in obj.transform)
            SaveObjectRecursive(child.gameObject, data);
    }
}
