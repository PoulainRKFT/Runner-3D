using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGUIBehavior : MonoBehaviour
{
    public void buttonStart() {
        SceneManager.LoadScene("Level");
    }

    public void buttonQuit() {
        Application.Quit();
    }
}
