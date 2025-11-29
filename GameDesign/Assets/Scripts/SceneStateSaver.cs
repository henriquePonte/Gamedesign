using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneStateSaver : MonoBehaviour
{
    public void SaveScene()
    {
        SceneSaveData data = new SceneSaveData();
        data.objects = new List<SceneObjectData>();

        GameObject[] allObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject root in allObjects)
        {
            SaveObjectRecursive(root, data);
        }

        // guardar a posição do jogador
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            data.playerPosition = player.transform.position;

        GameManager.instance.SaveCurrentScene(data);
        //Debug.Log($"SceneStateSaver: Cena '{SceneManager.GetActiveScene().name}' saved com sucesso!");
    }

    private void SaveObjectRecursive(GameObject obj, SceneSaveData data)
    {
        SceneObjectData objData = new SceneObjectData
        {
            objectName = obj.name,
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
