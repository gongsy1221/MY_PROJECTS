using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecEventManager : MonoBehaviour
{
    [SerializeField] GameObject selectEvent;

    [SerializeField] GameObject selectFirst;
    [SerializeField] GameObject selecSecond;

    [SerializeField] int selectEventNum;

    InteractionController theIC;
    DialogueManager dialogueManager;

    private void Awake()
    {
        theIC = FindObjectOfType<InteractionController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Start()
    {
        selectEvent.SetActive(true);
        theIC.SettingUI(false);
        dialogueManager.SettingUI(false);
    }

    public void SelecEvent()
    {
        selectEvent.SetActive(false);

        if(selectEventNum == 1)
        {
            selectFirst.SetActive(true);
        }
        else if(selectEventNum == 2)
        {
            selecSecond.SetActive(true);
        }
    }
}
