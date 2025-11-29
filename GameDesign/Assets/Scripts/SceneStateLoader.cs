using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneStateLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayedLoad());
    }

    private IEnumerator DelayedLoad()
    {
        // esperar cena carregar
        yield return null; 

        string sceneName = SceneManager.GetActiveScene().name;
        SceneSaveData data = GameManager.instance.GetSceneState(sceneName);

        if (data == null)
        {
            //Debug.Log($"SceneStateLoader: nenhum dado encontrado para a cena '{sceneName}'.");
            yield break;
        }

        // Restaurar objetos
        foreach (var objData in data.objects)
        {
            GameObject obj = GameObject.Find(objData.objectName);
            if (obj != null)
            {
                if (GameManager.instance.IsItemCollected(objData.objectName))
                    obj.SetActive(false);
                else
                {
                    obj.transform.position = objData.position;
                    obj.transform.rotation = objData.rotation;
                    obj.transform.localScale = objData.scale;
                    obj.SetActive(objData.isActive);
                }
            }
        }

        // Restaurar posição do Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = data.playerPosition;

        //Debug.Log($"SceneStateLoader: load concluído, {data.objects.Count} objetos restaurados.");
    }
}
