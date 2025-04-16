using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    public static bool isFinished = false;

    SplashManager splashManager;

    [SerializeField] Image img_CutSscene;

    private void Start()
    {
        splashManager = FindObjectOfType<SplashManager>();
    }

    public bool CheckCutScene()
    {
        return img_CutSscene.gameObject.activeSelf;
    }

    public IEnumerator CutSceneRoutine(string p_CutSceneName, bool p_isShow)
    {
        SplashManager.isfinished = false;
        StartCoroutine(splashManager.FadeOut(true, false));
        yield return new WaitUntil(() => SplashManager.isfinished);

        if (p_isShow)
        {
            Sprite t_sprite = Resources.Load<Sprite>("CutScene/" + p_CutSceneName);
            if (t_sprite != null)
            {
                img_CutSscene.gameObject.SetActive(true);
                img_CutSscene.sprite = t_sprite;
                
            }
            else
            {
                Debug.Log("NO CutScene");
            }
        }
        else
        {
            img_CutSscene.gameObject.SetActive(false);
        }

        SplashManager.isfinished = false;
        StartCoroutine(splashManager.FadeIn(true, false));
        yield return new WaitUntil(()=> SplashManager.isfinished);

        yield return new WaitForSeconds(0.5f);

        isFinished = true;
    }
}
