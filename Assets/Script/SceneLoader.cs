using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;


    private void Awake()
    {
        Instance = this;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
