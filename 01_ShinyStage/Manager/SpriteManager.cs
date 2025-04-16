using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] public SpriteRenderer dialogueImage;

    bool CheckSameSprite(SpriteRenderer p_spriteRenderer, Sprite p_sprite)
    {
        if(p_spriteRenderer.sprite == p_sprite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator SpriteChangeCoroutine(string p_SpriteName)
    {
        dialogueImage = dialogueImage.GetComponent<SpriteRenderer>();
        Sprite t_sprite = Resources.Load("Character/" + p_SpriteName, typeof(Sprite)) as Sprite;

        if(!CheckSameSprite(dialogueImage, t_sprite))
        {
            Color t_color = dialogueImage.color;
            t_color.a = 0;
            dialogueImage.color = t_color;

            dialogueImage.sprite = t_sprite;

            while(t_color.a < 1)
            {
                t_color.a += fadeSpeed;
                dialogueImage.color = t_color;
                yield return null;
            }
        }
    }
}
