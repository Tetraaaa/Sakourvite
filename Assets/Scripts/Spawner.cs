using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : Singleton<Spawner> {

    // Use this for initialization

    public GameObject enemy;
    public GameObject killableEnemy;
    List<float> spawnPointXs = new List<float>();
    Random rnd = new Random();
    public Coroutine spawnRoutine;

	void Start () {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            spawnPointXs.Add(-3f);
            spawnPointXs.Add(-1.5f);
            spawnPointXs.Add(0f);
            spawnPointXs.Add(1.5f);
            spawnPointXs.Add(3f);
        }
        else
        {
            spawnPointXs.Add(-3f);
            spawnPointXs.Add(0f);
            spawnPointXs.Add(3f);
        }

        spawnRoutine = StartCoroutine(SpawnEnemy());
    }

    public void StopSpawnRoutine()
    {
        StopCoroutine(spawnRoutine);
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            List<float> spawnPoints = new List<float>(spawnPointXs);

            int toDelete = Random.Range(0, spawnPointXs.Count);
            float lastSpawnPoint = spawnPoints[toDelete];
            spawnPoints.RemoveAt(toDelete);
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                Instantiate(enemy, new Vector3(15f, spawnPoints[i]), Quaternion.identity, gameObject.transform);
            }
            if (Random.Range(0f, 1f) > 0.8f * GameController.Instance.spawnFrequencyRatio) Instantiate(killableEnemy, new Vector3(15f, lastSpawnPoint), Quaternion.identity, gameObject.transform);
            
            yield return new WaitForSeconds(GameController.Instance.spawnFrequency*GameController.Instance.spawnFrequencyRatio);
        }

    }
}
