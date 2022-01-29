using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
// Variables
    [SerializeField]
    private GameObject enemies, markers, nutTreasure;

    private int enemiesCount;
    private bool shouldSpawn = true;
    private int randomEnemy;
    private int score = 0;
    // private int highScore = 0;

    void Start() {
        enemiesCount = enemies.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {   
        if(shouldSpawn)
            SpawnEnemy();
    }

// Function for stopping gameplay
    public void GameOver() {
        
        Time.timeScale = 0;
    }

// Increments the score for every point recieved
    public void IncrementScore(int value) {
        score += value;
    }

// Spawns random enemy and positions them at the target
    private void SpawnEnemy() {
        StartCoroutine(SpawnTimer());

        randomEnemy = Random.Range(0, enemiesCount);
        GameObject newEnemy = Instantiate(enemies.transform.GetChild(randomEnemy).gameObject);
        newEnemy.transform.position = markers.transform.GetChild(Random.Range(0,8)).position; 
        newEnemy.transform.LookAt(nutTreasure.transform, Vector3.up);

        // push the bird enemy up higher than the rest
        // if(randomEnemy == 3) 
            // newEnemy.transform.position += new Vector3(0, 2, 0);

            newEnemy.SetActive(true);
    }

// Timer for spawning enemy
    IEnumerator SpawnTimer() {
        shouldSpawn = false;
        yield return new WaitForSeconds((float)2);
        shouldSpawn = true;
    }
}
