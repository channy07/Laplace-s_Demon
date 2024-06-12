using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public Button startButton;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
