using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    [SerializeField] private GameObject ghostObj;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    private Player _player;

    private int minTime = 3;
    private int maxTime = 6;
    private int randTime = 5;
    private float currentTime = 0f;

    private bool currentState = true;
    private bool checkState = true;
    private bool isClear = false;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        ghostObj = _player.transform.Find("PlayerDie").gameObject;
    }

    private void Update()
    {
        if (door1.isKey || door2.isKey)
        {
            isClear = true;
            SoundManager.StopFx(audioClip);
        }

        if(!isClear)
        {
            currentTime += Time.deltaTime;

            if (currentTime > randTime)
            {
                SoundManager.StopFx(audioClip);
                StartCoroutine(ChangeLightObj());
                currentTime = 0f;
            }

            if (!checkState && _player.isMove)
            {
                _player.PlayerDie();
                ghostObj.SetActive(true);

                Invoke("RestartGame", 2.5f);
            }
        }
            
    }

    private IEnumerator ChangeLightObj()
    {
        randTime = Random.Range(minTime, maxTime);
        currentState = !currentState;
        lightObj.SetActive(currentState);
        if (!currentState)
            SoundManager.PlayFx(audioClip, 1, true);

        if(checkState)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return null;
        }
        checkState = currentState;
    }

    private void RestartGame()
    {
        RoomManager.Instance.RestartGame();
    }
}
