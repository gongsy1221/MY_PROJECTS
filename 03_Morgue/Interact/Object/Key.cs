using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public Item key;
    public AudioClip clip;

    public override void Interact()
    {
        Inventory.Instance.Add(key);
        SoundManager.PlayOnce(clip, 0.7f);
        Destroy(gameObject);
    }

    public override void Solved()
    {
    }
}
