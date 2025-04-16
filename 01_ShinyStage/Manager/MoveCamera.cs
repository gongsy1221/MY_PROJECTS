using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Drawing;
using System;
using Unity.VisualScripting;

using Color = UnityEngine.Color;
public class MoveCamera : MonoBehaviour
{
    [SerializeField] public Camera myCam;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject mapUI;

    InteractionController theIC;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
    }

    public void MyRoom()
    {
        CameraMove(new Vector3(20, 0, -10));
    }

    void CameraMove(Vector3 camPos)
    {
        mapUI.SetActive(false);
        myCam.transform.position = camPos;
        theIC.SettingUI(true);
    }

    IEnumerator FadeOut()
    {
        for(float a = 1f; a <= 1.0f;)
        {
            a += 0.2f;
            fadeImg.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeIn()
    {
        for (float a = 0f; a >= 0.0f;)
        {
            a -= 0.2f;
            fadeImg.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
