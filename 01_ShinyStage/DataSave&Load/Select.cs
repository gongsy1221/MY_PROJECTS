using System;
using System.IO;
using TMPro;
using UnityEngine;

public class Select : MonoBehaviour
{
    //public static Select instance;

    public TextMeshProUGUI[] slotText;

    bool[] savefile = new bool[5];

    Inventory inventory;

    DatabaseManager databaseManager;

    public bool loadScene = false;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {

        for (int i = 0; i < savefile.Length; i++)
        {
            try
            {
                if (File.Exists(DataManager.instance.path + $"{i}"))
                {
                    savefile[i] = true;

                    DataManager.instance.nowSlot = i;
                    DataManager.instance.LoadData();
                    slotText[i].text = DataManager.instance.nowPlayer.savetime;
                }
            }
            catch (FileNotFoundException e)
            {
                slotText[i].text = "비어있음";
                Debug.Log(e.Message);
            }
        }

        DataManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            GoGame();
        }
    }

    private void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.InitData();
            DataManager.instance.nowPlayer.savetime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            DataManager.instance.SaveData();
        }
        MySceneManager.Instance.SaveLoadScene(DataManager.instance.nowPlayer.sceneName);
    }

    public void LoadData()
    {
        inventory = FindObjectOfType<Inventory>();
        databaseManager = FindObjectOfType<DatabaseManager>();

        Camera.main.transform.position = DataManager.instance.nowPlayer.camPos;

        if (DataManager.instance.nowPlayer.eventFlags != null)
        {
            for (int i = 0; i < DataManager.instance.nowPlayer.eventFlags.Length; i++)
            {
                databaseManager.eventFlags[i] = DataManager.instance.nowPlayer.eventFlags[i];
            }
        }

        if (DataManager.instance.nowPlayer.items != null)
        {
            for (int i = 0; i < DataManager.instance.nowPlayer.items.Count; i++)
            {
                inventory.AddItem(DataManager.instance.nowPlayer.items[i]);
            }
        }

        MySceneManager.Instance.changeScene = false;
    }
}