using UnityEngine;
using System.Collections.Generic;

public class SceneStateSaver : MonoBehaviour
{
    public void SaveScene()
    {
        SceneSaveData data = new SceneSaveData();
        data.objects = new List<SceneObjectData>();

        // Pega TODOS os objetos da cena, ATIVOS e DESATIVADOS
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Ignorar objetos internos do Unity
            if (obj.hideFlags != HideFlags.None)
                continue;

            // Não guardar objetos que não estão na cena atual
            if (obj.scene.name == null || obj.scene.name == "")
                continue;

            SaveObject(obj, data);
        }

        // guardar posição do jogador
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            data.playerPosition = player.transform.position;

        // Enviar dados para o GameManager
        GameManager.instance.SaveCurrentScene(data);
    }

    private void SaveObject(GameObject obj, SceneSaveData data)
    {
        UniqueID unique = obj.GetComponent<UniqueID>();
        if (unique == null)
            return; // só guardamos objetos que têm UniqueID

        SceneObjectData objData = new SceneObjectData
        {
            objectName = unique.id,
            position = obj.transform.position,
            rotation = obj.transform.rotation,
            scale = obj.transform.localScale,
            isActive = obj.activeSelf
        };

        data.objects.Add(objData);
    }
}
