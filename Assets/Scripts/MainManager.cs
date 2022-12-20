using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //shares values in this class member with all instances of MainManager class
    public static MainManager Instance { get; private set; } //makes MainManager read-only outside of class, can edit in class

    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //can link MainManager.Instance without referencing it
        Instance = this;
        //marks MainManager object to not be destroyed on scene change
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    //serializes to JSON data
    [System.Serializable]
    class SaveData

    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        //creates new instance of SaveData
        SaveData data = new SaveData();
        //fills SaveData's team color with team color from MainManager
        data.TeamColor = TeamColor;

        //changes data to JSON format
        string json = JsonUtility.ToJson(data);

        //writes JSON data to specified file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        //finds defined path
        string path = Application.persistentDataPath + "/savefile.json";
        //if path exists
        if (File.Exists(path))
        {
            //reads text from file
            string json = File.ReadAllText(path);
            //takes json data from file and transforms back to SaveData instance
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //sets TeamColor to what was loaded from file
            TeamColor = data.TeamColor;
        }
    }
}
