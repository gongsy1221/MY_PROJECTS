using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class Game : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
    }

    public void Save()
    {
        DataManager.instance.nowPlayer.savetime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
        DataManager.instance.nowPlayer.sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < DatabaseManager.instance.eventFlags.Length; i++)
        {
            DataManager.instance.nowPlayer.eventFlags[i] = DatabaseManager.instance.eventFlags[i];
        }

        if (inventory != null)
        {
            DataManager.instance.nowPlayer.items = inventory.items;
        }

        DataManager.instance.nowPlayer.camPos = Camera.main.transform.position;
        DataManager.instance.SaveData();
    }
}
