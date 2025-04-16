using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalDoor : Interactable
{
    [SerializeField] private AudioClip closeAudioClip;
    [SerializeField] private AudioClip openAudioClip;

    public bool isOpen = false;
    public float doorOpenAngle = -90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2.5f;

    private void Awake()
    {
        //audioClip = Resources.Load("noise12") as AudioClip;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        //SoundManager.PlayEffect(audioClip);
        if (isOpen)
            SoundManager.PlayOnce(openAudioClip);
        else
            SoundManager.PlayOnce(closeAudioClip);
    }

    void Update()
    {
        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x, doorOpenAngle, transform.localRotation.y);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x, doorCloseAngle, transform.localRotation.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
    }

    public override void Solved()
    {
    }
}
