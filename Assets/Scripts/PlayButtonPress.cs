using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonPress : MonoBehaviour
{
    
    public void OnButtonPress()
    {
        Globals.score = 0;
        SceneManager.LoadScene("Maze");
    }
}
