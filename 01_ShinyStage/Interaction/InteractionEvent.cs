using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;

    [SerializeField] public DialogueEvent dialogueEvent;

    PolygonCollider2D polygonCollider2D;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool CheckEvent()
    {
        bool t_flag = true;

        for (int i = 0; i < dialogueEvent.eventTiming.eventConditions.Length; i++)
        {
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventConditions[i]] != dialogueEvent.eventTiming.conditionFlag)
            {
                t_flag = false;
                break;
            }
        }

        if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventEndNum])
        {
            t_flag = false;
        }

        return t_flag;
    }

    public Dialogue[] GetDialogues()
    {
        // 상호작용 후 대화
        if(DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] && dialogueEvent.isDialogueB)
        {
            dialogueEvent.dialoguesB = SettingDialogue(dialogueEvent.dialoguesB, (int)dialogueEvent.lineB.x, (int)dialogueEvent.lineB.y);
            return dialogueEvent.dialoguesB;
        }
        // 상호작용 전 대화
        else
        {
            DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] = true;
            dialogueEvent.dialogues = SettingDialogue(dialogueEvent.dialogues, (int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
            return dialogueEvent.dialogues;
        }
    }

    Dialogue[] SettingDialogue(Dialogue[] p_Dialogue, int p_lineX, int p_lineY)
    {
        Dialogue[] t_Dialogues = DatabaseManager.instance.GetDialogue(p_lineX, p_lineY);
        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_Dialogues[i].cameraType = p_Dialogue[i].cameraType;
        }

        return t_Dialogues;
    }

    public AppearType GetAppearType()
    {
        return dialogueEvent.appearType;
    }

    public GameObject[] GetTargets()
    {
        return dialogueEvent.go_Targets;
    }

    public GameObject GetNextEvent()
    {
        return dialogueEvent.go_NextEvent;
    }

    public int GetEventNumber()
    {
        return dialogueEvent.eventTiming.eventNum;
    }

    private void Update()
    {
        if (isAutoEvent && DatabaseManager.isFinish)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            DialogueManager.isWaiting = true;

            if (GetAppearType() == AppearType.Appear)
            {
                dialogueManager.SetAppearObjects(GetTargets());
            }
            else if (GetAppearType() == AppearType.Disappear)
            {
                dialogueManager.SetDisappearObjects(GetTargets());
            }
            dialogueManager.SetNextEvent(GetNextEvent());
            dialogueManager.ShowDialogue(GetDialogues());

            gameObject.SetActive(false);
        }

        if (polygonCollider2D != null && spriteRenderer != null)
        {
            bool t_flag = CheckEvent();

            polygonCollider2D.enabled = t_flag;
            spriteRenderer.enabled = t_flag;
        }
        else
        {
            return;
        }

    }
}
