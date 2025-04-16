using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform tf_Crosshair;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;


    }

    private void Update()
    {
        if(!InteractionController.isInteract)
        {
            CrosshairMoving();
        }
        
    }

    void CrosshairMoving()
    {
        tf_Crosshair.localPosition = new Vector2(Input.mousePosition.x - (Screen.width/2),
                                                 Input.mousePosition.y - (Screen.height/2));

        float t_cursorPosX = tf_Crosshair.localPosition.x;
        float t_cursorPosY = tf_Crosshair.localPosition.x;
    }
}
