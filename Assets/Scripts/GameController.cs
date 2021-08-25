using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int timeScale = 1;
    public void OnClickPause()
    {
        timeScale = Mathf.Abs(timeScale - 1);
        Time.timeScale = timeScale;
    }
}
