using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenDoor : MonoBehaviour
{
    [SerializeField] NormalDoor cabinet;

    private void Start()
    {
        cabinet.isOpen = true;
    }
}
