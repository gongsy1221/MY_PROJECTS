using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    [SerializeField] private AudioClip audioClip;

    private Player _player;

    private float currentY = -0.75f;
    private float maxY = 0.9f;

    private bool isClear = false;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SoundManager.PlayFx(audioClip, 1, true);
    }

    private void Update()
    {
        if (door1.isKey || door2.isKey)
        {
            isClear = true;
            SoundManager.StopFx(audioClip);
        }

        if (currentY <= maxY)
        {
            currentY += 0.0005f;
            water.transform.position =
                    new Vector3(water.transform.position.x, currentY, water.transform.position.z);
        }
        else
        {
            if(!isClear)
            {
                _player.PlayerDie();
                RestartGame();
            }
        }
    }

    private void RestartGame()
    {
        RoomManager.Instance.RestartGame();
    }
}
