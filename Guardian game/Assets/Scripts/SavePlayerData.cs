using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavePlayerData : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField] Player player;
    [SerializeField] PlayerData data;

    string persistentPath = "";

    void Start()
    {
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData(Object data)
    {
        Debug.Log("Saving data to: " + persistentPath);
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();
        Debug.Log(json);

        data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Data loaded: " + data.ToString());
    }

}
