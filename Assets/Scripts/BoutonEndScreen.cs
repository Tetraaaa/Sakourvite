using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoutonEndScreen : MonoBehaviour {
    Button b;
    
	// Use this for initialization
	void Start () {
        b = GetComponent<Button>();
        b.onClick.AddListener(ReturnToMainMenu);
	}
	
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
