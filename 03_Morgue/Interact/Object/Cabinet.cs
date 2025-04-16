using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cabinet : Interactable
{
    public bool open = false;
    public float cabinetOpenPos = -0.1f;
    public float cabinetClosePos = 0.3f;
    public float smoot = 2f;

    private void Awake()
    {
        //audioClip = Resources.Load("noise12") as AudioClip;
    }

    public override void Interact()
    {
        open = !open;
        //SoundManager.PlayEffect(audioClip);
    }

    void Update()
    {
        if (open)
        {
            Vector3 targetPos = new Vector3(cabinetOpenPos, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, smoot * Time.deltaTime);
        }
        else
        {
            Vector3 targetPos = new Vector3(cabinetClosePos, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, smoot * Time.deltaTime);
        }
    }

    public override void Solved()
    {
    }
}
