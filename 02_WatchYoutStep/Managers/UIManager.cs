using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Action onEnemySpawn;

    [SerializeField]
    private Sprite checkBox;
    [SerializeField]
    private Button bgmButton;
    [SerializeField]
    private Button sfxButton;
    [SerializeField]
    private List<Image> titlePanelItems = new List<Image>();
    [SerializeField]
    private TextMeshProUGUI narration;

    public Slider _musicSlider, _sfxSlider, _penaltySlider;

    public float dotTime = 0.5f;

    private RectTransform titlePanel;
    private RectTransform settingPanel;
    private RectTransform gameOverPanel;
    private RectTransform ingamePanel;
    private RectTransform explainPanel;
    private RectTransform startButton;
    private TextMeshProUGUI touchText;
    private Sprite originImg;
    private PlayerMovement player;

    [SerializeField] private Sprite[] miniHumanSprites;
    private Image miniHumanImage;

    private bool onBGM = true;
    private bool onSFX = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        titlePanel = GameObject.Find("TitlePanel").GetComponent<RectTransform>();
        settingPanel = GameObject.Find("SettingPanel").GetComponent<RectTransform>();
        gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<RectTransform>();
        ingamePanel = GameObject.Find("GamePlayPanel").GetComponent<RectTransform>();
        explainPanel = GameObject.Find("ExpainPanel").GetComponent<RectTransform>();
        startButton = GameObject.Find("Panel").GetComponent<RectTransform>();
        touchText = GameObject.Find("TouchText").GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<PlayerMovement>();

        miniHumanImage = GameObject.Find("MiniHuman").GetComponent<Image>();

        titlePanelItems = titlePanel.GetComponentsInChildren<Image>().ToList();
        titlePanelItems.RemoveAt(0);

    }

    private void Start()
    {
        originImg = bgmButton.image.sprite;
        settingPanel.DOScale(0, 0);
        gameOverPanel.DOScale(0, 0);
        ingamePanel.DOScale(0, 0);
        explainPanel.DOScale(0, 0);
    }

    public void ToggleMusic()
    {
        if (onBGM)
        {
            bgmButton.image.sprite = checkBox;
            onBGM = false;
        }
        else if (!onBGM)
        {
            bgmButton.image.sprite = originImg;
            onBGM = true;
        }

        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        if (onSFX)
        {
            sfxButton.image.sprite = checkBox;
            onSFX = false;
        }
        else if (!onSFX)
        {
            sfxButton.image.sprite = originImg;
            onSFX = true;
        }

        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void SettingButton() // ����â Ű��
    {
        AudioManager.Instance.PlaySFX("ButtonSound");
        GameManager.Instance.isPanelOpen = true;
        startButton.gameObject.SetActive(false);
        settingPanel.DOScale(1, dotTime).SetEase(Ease.InSine);
    }

    public void XButton() // ����â ����
    {
        AudioManager.Instance.PlaySFX("ButtonSound");
        GameManager.Instance.isPanelOpen = false;
        startButton.gameObject.SetActive(true);
        settingPanel.DOScale(0, dotTime).SetEase(Ease.InSine);
    }

    public void ExitButton() // ���� ����
    {
        Application.Quit();
    }

    public void HomeButton() // Ÿ��Ʋ ȭ������ �̵�
    {
        CameraManager.Instance.TitleCamera();
        ScoreManager.Instance.ResetScore();
        player.transform.position = player.playerOriginTransform.position;
        OffGameOver();
        OnTitlePanel();
        MapManager.Instance.Resetting();
        MapManager.Instance.Starting();
    }

    public void RestartButton() // ���� �����
    {
        onEnemySpawn?.Invoke();

        CameraManager.Instance.FollowingCamera();
        GameManager.Instance.GameStart();
        ScoreManager.Instance.ResetScore();
        player.transform.position = player.playerOriginTransform.position;
        OffGameOver();
        OnInGamePanel();
        MapManager.Instance.Resetting();
        MapManager.Instance.Starting();
        SliderValueChangeing();
    }

    public void OnGameOver(bool isTimeOver = false) // ���ӿ��� ����
    {
        gameOverPanel.DOScale(1, dotTime).SetEase(Ease.InSine);

        if (isTimeOver)
        {
            miniHumanImage.sprite = miniHumanSprites[0];
            RandomText();
        }
        else
        {
            miniHumanImage.sprite = miniHumanSprites[1];
            RandomDeadText();
        }
    }

    public void OffGameOver() // ���ӿ��� ����
    {
        AudioManager.Instance.PlaySFX("ButtonSound");
        gameOverPanel.DOScale(0, dotTime).SetEase(Ease.InSine);
    }

    public void OnInGamePanel()
    {
        ingamePanel.DOScale(1, dotTime).SetEase(Ease.InSine);
    }

    public void OffInGamePanel()
    {
        ingamePanel.DOScale(0, dotTime).SetEase(Ease.InSine);
    }

    public void OnExplainPanel()
    {
        AudioManager.Instance.PlaySFX("ButtonSound");
        GameManager.Instance.isPanelOpen = true;
        startButton.gameObject.SetActive(false);
        explainPanel.DOScale(1, dotTime).SetEase(Ease.InSine);
    }

    public void OffExplainPanel()
    {
        AudioManager.Instance.PlaySFX("ButtonSound");
        GameManager.Instance.isPanelOpen = false;
        startButton.gameObject.SetActive(true);
        explainPanel.DOScale(0, dotTime).SetEase(Ease.InSine);
    }

    public void OnTitlePanel()
    {
        foreach (var item in titlePanelItems)
        {
            item.DOFade(1, 1);
        }

        touchText.DOFade(1, 1);
        startButton.gameObject.SetActive(true);
    }

    public void OffTitlePanel()
    {
        foreach (var item in titlePanelItems)
        {
            item.DOFade(0, 1);
        }

        touchText.DOFade(0, 1);
        startButton.gameObject.SetActive(false);
    }

    private void RandomText()
    {
        string[] narrations =
            { "����?", "������ �̻��ϳ�", "Ÿ�� �������� ����", "��¥ ���Ѵ� ����",
            "���� ������~", "�̰� �׳�", "����!!", "��������������"};

        int rd = Random.Range(0, narrations.Length);

        narration.text = narrations[rd];
    }

    private void RandomDeadText()
    {
        string[] narrations =
        { "�����", "���ſ�", "�� ����\n����..", "�ФѤ�", "�װ� ����\n�� �׿�",
            "��", "����\n�׾��ٴ�...", "�� ���� ����...", "��...�ФѤ�", "�� ���� ���ξ�",
            "�ФѤ�", "�ƾ�..", "�ʹ���", "�� ���", "�������Ÿ�\n�� �� ��������...", "�ʹ� �����ݾ�"
        };

        int rd = Random.Range(0, narrations.Length);

        narration.text = narrations[rd];
    }

    public void SliderValueChangeing()
    {
        StopCoroutine("Penalting");
        StartCoroutine("Penalting");
        _penaltySlider.gameObject.SetActive(true);
        _penaltySlider.value = _penaltySlider.maxValue;
    }

    public void StopSliderValueChange()
    {
        StopAllCoroutines();
        _penaltySlider.value = _penaltySlider.maxValue;
        _penaltySlider.gameObject.SetActive(false);
    }

    public IEnumerator Penalting()
    {
        while (_penaltySlider.value != 0)
        {
            _penaltySlider.value -= Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.GameOver(true);
    }
}
