using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Item key;
    public Door pairDoor;
    public AudioClip audioClip;
    public AudioClip clostAudioClip;
    private AudioClip doorHandleAudio;

    public bool isKey = false;
    public bool isOpen = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;

    private void Awake()
    {
        //doorHandleAudio = Resources.Load("DoorHandle") as AudioClip;
    }

    public override void Interact()
    {
        List<Item> items = Inventory.Instance.GetItems();
        if (items.Count > 0 && items[0] == key && !isKey)
        {
            //TimerManager.Instance.StopTimer();
            Inventory.Instance.Remove(key);
            isKey = true;
            pairDoor.isKey = true;
            promptMessage = "문";
            pairDoor.promptMessage = promptMessage;
        }
        else if (isKey)
        {
            isOpen = !isOpen;
            pairDoor.isOpen = isOpen;
            SoundManager.PlayOnce(audioClip);
            SoundManager.PlayOnce(pairDoor.audioClip);
        }
        else
        {
            //SoundManager.PlayEffect(doorHandleAudio);
        }
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

        if (pairDoor != null)
        {
            if (pairDoor.isOpen)
            {
                Quaternion targetRotation =
                    Quaternion.Euler(pairDoor.transform.localRotation.x, pairDoor.doorOpenAngle, pairDoor.transform.localRotation.y);
                pairDoor.transform.localRotation =
                    Quaternion.Slerp(pairDoor.transform.localRotation, targetRotation, smoot * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotation =
                    Quaternion.Euler(pairDoor.transform.localRotation.x, pairDoor.doorCloseAngle, pairDoor.transform.localRotation.z);
                pairDoor.transform.localRotation =
                    Quaternion.Slerp(pairDoor.transform.localRotation, targetRotation, smoot * Time.deltaTime);
            }
        }
    }

    public void CloseDoor()
    {
        isOpen = false;
        pairDoor.isOpen = false;
        isKey = false;
        pairDoor.isKey = false;
        SoundManager.PlayOnce(clostAudioClip);
    }

    public override void Solved()
    {
    }
}
