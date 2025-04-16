using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string savetime;
    public string sceneName;
    public bool[] eventFlags;
    public List<Item> items;
    public Vector3 camPos;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public SaveData nowPlayer = new SaveData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";
        print(path);
        
        //SaveData();
        //LoadData();
        //Debug.Log(nowPlayer.savetime);
    }

    public void InitData()
    {
        nowPlayer.savetime = "0";
        nowPlayer.sceneName = "01_First";
        nowPlayer.eventFlags = new bool[100];
        nowPlayer.items = new List<Item>();
        nowPlayer.camPos = new Vector3(0, 0, -10);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<SaveData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new SaveData();
    }
}
