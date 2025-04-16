using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public CanvasGroup Fade_img;
    float fadeDuration = 2;

    public TextMeshProUGUI endingText;
    float speed = 0.2f;

    public GameObject Loading;
    public TextMeshProUGUI Loading_text;

    public bool changeScene = false;
    public bool typingEnd;

    Select select;

    private void Awake()
    {
        select = FindObjectOfType<Select>();
    }


    public static MySceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static MySceneManager instance;

    void Start()
    {
        changeScene = false;
        typingEnd = false;
        endingText.enabled = false;

        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
        });
    }

    public void ChangeScene(string sceneName, string stageName = "")
    {
        SoundManager.instance.StopBGM();
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true;
        })
        .OnComplete(() => {
            StartCoroutine(TypingScene(sceneName, stageName));
            SoundManager.instance.PlaySound("Check", 1);
        });
    }

    public void SaveLoadScene(string sceneName)
    {
        SoundManager.instance.StopBGM();
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true;
        })
        .OnComplete(() => {
            StartCoroutine("LoadScene", sceneName);
            select.LoadData();
            SoundManager.instance.PlaySound("Check", 1);
        });
    }

    public void EndingCredit(string endingName)
    {
        SoundManager.instance.StopBGM();
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true;
            
        })
        .OnComplete(() => {
            StartCoroutine(TypingEnding(endingName));
        });
    }

    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            Loading_text.text = percentage.ToString("0") + "%";
        }

        
    }

    IEnumerator TypingEnding(string message)
    {
        endingText.enabled = true;
        message = message.Replace("\\n", "\n");

        for (int i = 0; i < message.Length; i++)
        {
            endingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(0.5f);
        endingText.enabled = false;
        typingEnd = true;
        StartCoroutine("LoadScene", "00_StartMenu");
    }
    
    IEnumerator TypingScene(string sceneName,string message)
    {
        endingText.enabled = true;
        message = message.Replace("\\n", "\n");

        for (int i = 0; i < message.Length; i++)
        {
            endingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(0.5f);
        endingText.enabled = false;
        typingEnd = true;
        StartCoroutine("LoadScene", sceneName);
        changeScene = true;
    }
}
