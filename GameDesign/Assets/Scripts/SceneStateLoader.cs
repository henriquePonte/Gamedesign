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
        yield return null; // esperar 1 frame

        string sceneName = SceneManager.GetActiveScene().name;
        SceneSaveData data = GameManager.instance.GetSceneState(sceneName);

        if (data == null)
            yield break;

        // Lista de todos os objetos com UniqueID na cena
        UniqueID[] allIDs = GameObject.FindObjectsOfType<UniqueID>(true); 

        foreach (var objData in data.objects)
        {
            foreach (var id in allIDs)
            {
                if (id.id == objData.objectName)
                {
                    GameObject obj = id.gameObject;

                    // ----- ITENS -----
                    if (obj.CompareTag("Item"))
                    {
                        if (GameManager.instance.IsItemCollected(id.id))
                            obj.SetActive(false);
                        else
                            obj.SetActive(objData.isActive);

                        break;
                    }

                    // ----- OBJETOS NORMAIS -----

                    // 1) Primeiro ativa/desativa
                    obj.SetActive(objData.isActive);

                    // 2) Depois restaura transform
                    obj.transform.position = objData.position;
                    obj.transform.rotation = objData.rotation;
                    obj.transform.localScale = objData.scale;

                    break;
                }
            }
        }

        // Restaurar posição do Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = data.playerPosition;
    }
}
