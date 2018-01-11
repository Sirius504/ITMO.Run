using EZObjectPools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

    public EZObjectPool[] objectPool;
    public float startWait;
    public float minDelay;
    public float maxDelay;
    public Vector2 spawnValues;
    public bool isSpawning = false;

    public GameState gameState;

    public enum GameState
    {
        StartScreen,
        Running,
        PauseScreen,
        DeadScreen
    };


    // Use this for initialization
    void Start () {
        gameState = GameState.StartScreen;
        
	}

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (isSpawning)
            {
                int i = Random.Range(0, objectPool.Length);
                objectPool[i].TryGetNextObject(spawnValues, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    public void StartGame()
    {
        gameState = GameState.Running;
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
        
    }
}
