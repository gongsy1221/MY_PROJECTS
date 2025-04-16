using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
//using UnityEditor.PackageManager;

public class DialogueManager : MonoBehaviour
{
    public static bool isWaiting = false;

    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;

    Dialogue[] dialogues;

    public bool isDialogue = false;
    bool isNext = false;

    [Header ("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대사 카운트

    //다음 이벤트 변수
    GameObject go_NextEvent;

    public void SetNextEvent(GameObject p_NextEvent)
    {
        go_NextEvent = p_NextEvent;
    }

    // 이벤트 끝나면 등장, 퇴장 오브젝트들
    GameObject[] go_Objects;
    byte appearTypeNumber;
    const byte none = 0, appear = 1, disappear = 2;

    public void SetAppearObjects(GameObject[] p_targets)
    {
        go_Objects = p_targets;
        appearTypeNumber = appear;
    }

    public void SetDisappearObjects(GameObject[] p_targets)
    {
        go_Objects = p_targets;
        appearTypeNumber = disappear;
    }

    InteractionController theIC;
    SplashManager splashManager;
    SpriteManager spriteManager;
    CutSceneManager cutSceneManager;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        splashManager = FindObjectOfType<SplashManager>();
        spriteManager = FindObjectOfType<SpriteManager>();
        cutSceneManager = FindObjectOfType<CutSceneManager>();
    }

    private void Update()
    {
        if(isDialogue)
        {
            if(isNext)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount < dialogues.Length)
                        {
                            StartCoroutine(CameraTargettingType());
                        }
                        else
                        {
                            StartCoroutine(EndDialogue());
                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        theIC.SettingUI(false);
        dialogues = p_dialogues;

        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        if(isWaiting)
        {
            yield return new WaitForSeconds(0.5f);
        }
        isWaiting = false;
        StartCoroutine(CameraTargettingType());
    }

    IEnumerator CameraTargettingType()
    {
        switch(dialogues[lineCount].cameraType)
        {
            case CameraType.FadeIn: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.FadeIn(false, true));yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FadeOut: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashIn: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.Splash()); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashOut: go_DialogueNameBar.SetActive(false); SettingUI(false);SplashManager.isfinished = false; StartCoroutine(splashManager.Splash()); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.ShowCutScene: SettingUI(false); CutSceneManager.isFinished = false;StartCoroutine(cutSceneManager.CutSceneRoutine(dialogues[lineCount].spriteName[contextCount], true));yield return new WaitUntil(() => CutSceneManager.isFinished);break;
            case CameraType.HideCutScene: SettingUI(false); CutSceneManager.isFinished = false;StartCoroutine(cutSceneManager.CutSceneRoutine(null, false));yield return new WaitUntil(() => CutSceneManager.isFinished);break;
        }

        StartCoroutine(TypeWriter());
    }

    IEnumerator EndDialogue()
    {
        if(cutSceneManager.CheckCutScene())
        {
            CutSceneManager.isFinished = false;
            StartCoroutine(cutSceneManager.CutSceneRoutine(null, false));
            yield return new WaitUntil(() => CutSceneManager.isFinished);
        }

        AppearOrDisappearObjects();

        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        SettingUI(false);
        go_DialogueNameBar.SetActive(false);
        yield return new WaitUntil(() => !InteractionController.isInteract);

        theIC.GetItem();

        if(go_NextEvent != null)
        {
            go_NextEvent.SetActive(true);
            go_NextEvent = null;
        }

    }

    void AppearOrDisappearObjects()
    {
        if(go_Objects!=null)
        {
            for (int i = 0; i < go_Objects.Length; i++)
            {
                if(appearTypeNumber == appear)
                {
                    go_Objects[i].SetActive(true);
                }
                else if(appearTypeNumber == disappear)
                {
                    go_Objects[i].SetActive(false);
                }
            }
        }
        go_Objects = null;
        appearTypeNumber = none;
    }

    void ChangeSprite()
    {
        if (spriteManager.dialogueImage != null)
        {
            if (dialogues[lineCount].spriteName[contextCount] != "")
            {
                StartCoroutine(spriteManager.SpriteChangeCoroutine(dialogues[lineCount].spriteName[contextCount]));
            }
        }
    }

    void PlaySound()
    {
        if (dialogues[lineCount].voiceName[contextCount] != "")
        {
            SoundManager.instance.PlaySound(dialogues[lineCount].voiceName[contextCount], 2);
        }
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        ChangeSprite();
        PlaySound();

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        bool t_black = false, t_red = false, t_cyan = false;
        bool t_ignore = false; 

        //txt_Dialogue.text = dialogues[lineCount].name;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch(t_ReplaceText[i])
            {
                case 'ⓑ':t_black = true; t_red = false; t_cyan = false; t_ignore = true; break;
                case 'ⓡ':t_black = false; t_red = true; t_cyan = false; t_ignore = true; break;
                case 'ⓒ':t_black = false; t_red = false; t_cyan = true; t_ignore = true; break;
                case '①': SoundManager.instance.PlaySound("Step", 1); t_ignore = true; break;

            }

            string t_letter = t_ReplaceText[i].ToString();

            if(!t_ignore)
            {
                if (t_black) { t_letter = "<color=#000000>" + t_letter + "</color>"; }
                else if (t_red) { t_letter = "<color=#FF000B>" + t_letter + "</color>"; }
                else if (t_cyan) { t_letter = "<color=#0038FF>" + t_letter + "</color>"; }
                txt_Dialogue.text += t_letter;
            }
            t_ignore = false;

            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
    }

    public void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);

        spriteManager.dialogueImage.enabled = p_flag;
        //go_DialogueNameBar.SetActive(p_flag);

        if(p_flag)
        {
            if (dialogues[lineCount].name == "")
            {
                go_DialogueNameBar.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
        else
        {
            go_DialogueNameBar.SetActive(false);
        }
    }
}
