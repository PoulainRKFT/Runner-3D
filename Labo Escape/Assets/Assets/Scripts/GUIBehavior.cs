using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIBehavior : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    public TextMeshProUGUI distanceValue;
    public TextMeshProUGUI yellowDocuments;
    public TextMeshProUGUI blueDocuments;
    public TextMeshProUGUI scoreText;

    public void buttonRestart() {
        SceneManager.LoadScene("Level");
    }

    public void buttonQuit() {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + (int)(playerBehavior.gameObject.transform.position.z + (playerBehavior.yellowDocuments * 100) + (playerBehavior.blueDocuments * 5000));
        distanceValue.text = "" + (int)playerBehavior.transform.position.z;
        yellowDocuments.text = "" + playerBehavior.yellowDocuments;
        blueDocuments.text = "" + playerBehavior.blueDocuments;
    }
}
