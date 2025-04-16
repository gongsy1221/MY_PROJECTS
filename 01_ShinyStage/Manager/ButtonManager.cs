using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject openButton;
    [SerializeField] GameObject closeButton;
    [SerializeField] GameObject mapUI;
    [SerializeField] GameObject settingUI;

    InteractionController theIC;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();

        mapUI.SetActive(false);
    }

    public void ClickMoveButton()
    {
        mapUI.SetActive(true);
        theIC.SettingUI(false);
    }

    public void ExitMove()
    {
        mapUI.SetActive(false);
        theIC.SettingUI(true);
    }

    public void ClickCloseButton()
    {
        StartCoroutine(MoveDown());
    }

    public void ClickOpenButton()
    {
        MoveUp();
    }

    IEnumerator MoveDown()
    {
        Vector3 endPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 3, 20);
        transform.DOMove(endPos, 0.5f);
        yield return new WaitForSeconds(0.5f);
        closeButton.SetActive(false);
        openButton.SetActive(true);
    }

    void MoveUp()
    {
        Vector3 startPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 20);
        openButton.SetActive(false);
        closeButton.SetActive(true);
        transform.DOMove(startPos, 0.5f);
    }

    public void GoStartScene()
    {
        SceneManager.LoadScene("00_StartMenu");
    }

    public void ClickSettingButton()
    {
        settingUI.SetActive(true);
        theIC.SettingUI(false);
    }

    public void ExitStting()
    {
        settingUI.SetActive(false);
        theIC.SettingUI(true);
    }
}
