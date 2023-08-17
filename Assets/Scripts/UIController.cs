using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController:MonoBehaviour{

    private static Canvas UI;
    private static Canvas gameOverUI;
    private static Canvas levelCompletedUI;

    private static Slider progressBar;
    private static Button retryButton;
    private static Text endText;

	// Use this for initialization

    void Awake()
    {
        UI = GetComponentsInChildren<Canvas>()[0];
        gameOverUI = GetComponentsInChildren<Canvas>()[1];
        levelCompletedUI = GetComponentsInChildren<Canvas>()[2];

        progressBar = UI.GetComponentInChildren<Slider>();
        retryButton = gameOverUI.GetComponentInChildren<Button>();
        endText = levelCompletedUI.GetComponentInChildren<Text>();

        retryButton.onClick.AddListener(OnRetryClick);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public static void ChangeToGameOverUI()
    {
        gameOverUI.enabled = true;
    }

    public static void ChangeToLevelCompletedUI()
    {
        levelCompletedUI.enabled = true;
    }

    public static void UpdateLevelProgression(float newValue)
    {
        if(progressBar != null) progressBar.value = newValue;
    }

    private void OnRetryClick()
    {
        gameOverUI.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
