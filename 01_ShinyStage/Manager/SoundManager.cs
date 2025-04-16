using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] Sound[] effectSounds;
    [SerializeField] AudioSource[] effectPlayer;

    [SerializeField] Sound[] bgmSounds;
    [SerializeField] AudioSource bgmPlayer;

    [SerializeField] AudioSource voicePlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySound("StartMenu", 0);
    }

    void PlayBGM(string p_name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if(p_name == bgmSounds[i].name)
            {
                bgmPlayer.clip = bgmSounds[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.Log("NO Name BGM");
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PauseBGM()
    {
        bgmPlayer.Pause();
    }

    public void UnPauseBGM()
    {
        bgmPlayer.UnPause();
    }

    void PlayEffectSound(string p_name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(p_name == effectSounds[i].name)
            {
                for (int j = 0; j < effectPlayer.Length; j++)
                {
                    if (!effectPlayer[j].isPlaying)
                    {
                        effectPlayer[j].clip = effectSounds[i].clip;
                        effectPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("All Sound Use");
            }
        }
        Debug.Log("No Effect Sound");
    }

    void StopAllEffect()
    {
        for (int i = 0; i < effectPlayer.Length; i++)
        {
            effectPlayer[i].Stop();
        }
    }

    void PlayVoiceSound(string p_name)
    {
        AudioClip _clip = Resources.Load<AudioClip>("Sounds/" + p_name);
        if(_clip != null )
        {
            voicePlayer.clip = _clip;
            voicePlayer.Play();
        }
        else
        {
            Debug.LogError(p_name + " is no sound");
        }
    }

    public void PlaySound(string p_name, int p_type)
    {
        if (p_type == 0) PlayBGM(p_name);
        else if (p_type == 1) PlayEffectSound(p_name);
        else if(p_type == 2) PlayVoiceSound(p_name);
    }
}
