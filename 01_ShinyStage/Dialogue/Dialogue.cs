using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CameraType
{
    None,
    FadeIn,
    FadeOut,
    FlashIn,
    FlashOut,
    ShowCutScene,
    HideCutScene
}

public enum AppearType
{
    None,
    Appear,
    Disappear
}

[System.Serializable]
public class Dialogue
{
    [Tooltip("대화하고 있는 캐릭터 이미지")]
    public CameraType cameraType;

    [HideInInspector]
    public string name;

    [HideInInspector]
    public string[] contexts;

    [Tooltip("이벤트 번호")]
    public string[] number;

    [Tooltip("스킵라인")]
    public string[] skipnum;

    [HideInInspector]
    public string[] spriteName;

    [HideInInspector]
    public string[] voiceName;
}

[System.Serializable]
public class EventTiming
{
    public int eventNum;
    public int[] eventConditions;
    public bool conditionFlag;
    public int eventEndNum;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;
    public EventTiming eventTiming;

    public Vector2 line;
    public Dialogue[] dialogues;

    [Space]
    public bool isDialogueB;
    public Vector2 lineB;
    public Dialogue[] dialoguesB;

    [Space]
    public AppearType appearType;
    public GameObject[] go_Targets;

    [Space]
    public GameObject go_NextEvent;

    [Space]
    public bool isItem;
}
