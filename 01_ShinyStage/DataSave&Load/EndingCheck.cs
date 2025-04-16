using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCheck : MonoBehaviour
{
    [SerializeField] GameObject cmwWin;
    [SerializeField] GameObject kyWin;

    private void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10f);

        if(CardGameData.instance.nowPlayer.cmwSetScore >= 3)
        {
            cmwWin.SetActive(true);
        }
        else if(CardGameData.instance.nowPlayer.kySetScore >= 3)
        {
            kyWin.SetActive(true);
        }

        Debug.Log("Play");
    }
}
