using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.HasKey("NowScene"))
        {
            // nếu có NowScene thì gán LastScene = NowScene, NowScene = Scene hiện tại
            PlayerPrefs.SetString("LastScene", PlayerPrefs.GetString("NowScene"));
            PlayerPrefs.SetString("NowScene", SceneManager.GetActiveScene().name);
        }
        else
        {
            // nếu không có NowScene thì gán NowScene = LastScene = Scene hiện tại
            PlayerPrefs.SetString("NowScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        }
    }
}
