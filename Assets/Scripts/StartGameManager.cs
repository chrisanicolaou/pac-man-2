using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame() 
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.7f);
        Time.timeScale = 1f;
    }
}
