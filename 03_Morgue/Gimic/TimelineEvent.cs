using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;

    private bool isPlay = false;

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
            lights[i].GetComponent<SplashLight>().enabled = false;
        isPlay = false;

        StartCoroutine(EyeSplash());
    }

    private void Update()
    {
        if (isPlay)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].GetComponent<SplashLight>().enabled = true;
            isPlay = false;
        }
    }

    private IEnumerator EyeSplash()
    {
        FadeManager.Instance.FadeOut();
        yield return null;
        FadeManager.Instance.FadeIn();
        yield return null;
        FadeManager.Instance.FadeOut();
    }

    public void PlayOn()
    {
        isPlay = true;
    }

    public void PlaySound()
    {

    }

    public void LoadScene()
    {
        FadeManager.Instance.Alpah1();
        StartCoroutine(LoadSceneRoutine("01_MainScene"));
    }

    public void LoadEndScene()
    {
        FadeManager.Instance.Alpah1();
        StartCoroutine(LoadSceneRoutine("00_TitleScene"));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
