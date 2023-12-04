using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_To_Main : MonoBehaviour
{
    public Button StartButton; // reference to the button

    void Start()
    {
        StartButton.onClick.AddListener(OnButtonClick); // add an event listener
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene("Main"); // load the scene
    }
}
