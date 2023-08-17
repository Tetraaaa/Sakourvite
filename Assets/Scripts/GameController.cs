using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController> {

    public float levelOneProgression = 1f;
    public float levelOneSpeedRatio = 1f;
    public float spawnFrequency = 1.5f;
    public float spawnFrequencyRatio = 1.0f;
    public Animator camAnim;

    public Coroutine gameCoroutine;


	// Use this for initialization
	void Start () {
        gameCoroutine = StartCoroutine(Game());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LowerSpeedOnce()
    {
        levelOneSpeedRatio *= 0.6f;
        if (levelOneSpeedRatio < 1f) levelOneSpeedRatio = 1f;
        spawnFrequencyRatio *= 1.4f;
        if (spawnFrequencyRatio > 1.0f) spawnFrequencyRatio = 1.0f;
    }

    IEnumerator Game()
    {
        while (true)
        {
            if (levelOneSpeedRatio < 4.5f) levelOneSpeedRatio += 0.05f;
            levelOneProgression += 0.05f;
            if (spawnFrequencyRatio > 0.5f) spawnFrequencyRatio -= 0.0111f;
            UIController.UpdateLevelProgression(levelOneProgression);

            if (levelOneProgression > 3.4f) NextLevel();
            yield return new WaitForSeconds(spawnFrequency * spawnFrequencyRatio);
        }
    }

    public void ScreenShake()
    {
        camAnim.SetTrigger("shake");
    }

    private void StopBackgroundScrolling()
    {
        foreach(BackgroundLoader b in GameObject.FindGameObjectWithTag("Scrolling").GetComponents<BackgroundLoader>())
        {
            b.speed = 0f;
        }
        
    }

    public void GameOver()
    {
        Destroy(Spawner.Instance.gameObject);
        StopCoroutine(gameCoroutine);
        StopBackgroundScrolling();

        levelOneProgression = 1f;
        levelOneSpeedRatio = 1f;
        spawnFrequency = 1.5f;
        spawnFrequencyRatio = 1.0f;
        UIController.ChangeToGameOverUI();
    }

    public void NextLevel()
    {
        Spawner.Instance.StopSpawnRoutine();
        StartCoroutine(EndOfLevelScene());
    }

    private IEnumerator EndOfLevelScene()
    {
        UIController.ChangeToLevelCompletedUI();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
