using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipGame : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.F1))
        {
            SceneManager.LoadScene(2);
        }
        else if(Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadScene(3);
        }
        else if(Input.GetKeyUp(KeyCode.F3))
        {
            SceneManager.LoadScene(4);
        }
        else if(Input.GetKeyUp(KeyCode.F4))
        {
            SceneManager.LoadScene(5);
        }
    }
}
