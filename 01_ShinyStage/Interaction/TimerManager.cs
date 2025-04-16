using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float limitTime;
    public TextMeshProUGUI time;

    [SerializeField] GameObject badEndingEvent;

    private void Update()
    {
        limitTime -= Time.deltaTime;
        time.text = Mathf.Round(limitTime) + "√ ";

        if(Mathf.Round(limitTime) == 0)
        {
            gameObject.SetActive(false);
            badEndingEvent.SetActive(true);
        }

        if (DatabaseManager.instance.eventFlags[9])
        {
            gameObject.SetActive(false);
        }
    }
}
