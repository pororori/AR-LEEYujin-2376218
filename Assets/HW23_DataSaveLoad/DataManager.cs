using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TransformData
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
}

[System.Serializable]
public class WorldData
{
    public List<TransformData> objects = new List<TransformData>();
}

public class DataManager : MonoBehaviour
{
    public List<GameObject> targetObjects; // Player, ObjectA, ObjectB 연결
    
    private string savePath;
    private WorldData worldData = new WorldData();

    void Start()
    {
        savePath = Application.persistentDataPath + "/worlddata.json";
        Debug.Log("Save Path: " + savePath);
    }

    public void Save()
    {
        worldData.objects.Clear();

        foreach (GameObject obj in targetObjects)
        {
            TransformData data = new TransformData();
            data.name = obj.name;
            data.position = obj.transform.position;
            data.rotation = obj.transform.eulerAngles;
            worldData.objects.Add(data);
        }

        string json = JsonUtility.ToJson(worldData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Saved: " + json);
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found!");
            return;
        }

        string json = File.ReadAllText(savePath);
        worldData = JsonUtility.FromJson<WorldData>(json);

        foreach (TransformData data in worldData.objects)
        {
            foreach (GameObject obj in targetObjects)
            {
                if (obj.name == data.name)
                {
                    obj.transform.position = data.position;
                    obj.transform.eulerAngles = data.rotation;
                }
            }
        }
        Debug.Log("Loaded!");
    }
}