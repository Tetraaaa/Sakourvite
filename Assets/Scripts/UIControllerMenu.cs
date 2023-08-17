using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerMenu : MonoBehaviour {

    public Canvas menuCanvas;

    private static Button startButton;

	// Use this for initialization
    void Awake()
    {
        startButton = menuCanvas.GetComponentInChildren<Button>();
        startButton.onClick.AddListener(OnStartGame);
    }
	

    private void OnStartGame()
    {
        menuCanvas.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(1);
    }
}
