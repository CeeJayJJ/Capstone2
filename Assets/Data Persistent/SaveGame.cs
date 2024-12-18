using UnityEngine;

public class SaveGame
{

    //serialized
    public string PlayerName = "Player";
    public float techbar = 0;
    public Vector3 PlayerPosition = Vector3.zero;
    public string currentScene = "";
    private static string _gameDataFileName = "data.json";

    private static SaveGame _instance;
    public static SaveGame Instance
    {
        get
        {
            if (_instance == null)
                Load();
            return _instance;
        }

    }

    public static void Save()
    {
        FileManager.Save(_gameDataFileName, _instance);
    }

    public static void Load()
    {
        _instance = FileManager.Load<SaveGame>(_gameDataFileName);
    }

}
