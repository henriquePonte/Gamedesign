using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneObjectData
{
    public string objectName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public bool isActive;
    public Dictionary<string, string> customData = new Dictionary<string, string>();
}

[Serializable]
public class SceneSaveData
{
    public List<SceneObjectData> objects = new List<SceneObjectData>();
    public Vector3 playerPosition;
}
