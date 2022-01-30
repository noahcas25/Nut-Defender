using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                  
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
// Variables
    [SerializeField]
    private GameObject player, enemies, markers, nutTreasure;

// UI variables
    [SerializeField]
    private GameObject highScoreUI, scoreUI, gameOverCanvas;

    private int enemiesCount;
    private bool shouldSpawn = true;
    private int randomEnemy;
    private int score = 0;
    private int highScore = 0;

    void Start() {
        Application.targetFrameRate = 60;
        enemiesCount = enemies.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {   
        if(shouldSpawn)
            SpawnEnemy();
    }

    void OnEnable() {
        if(PlayerPrefs.HasKey("HighScore")) {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

// Function for stopping gameplay
    public void GameOver() {
        Time.timeScale = 0f;
        player.GetComponent<PlayerControllerScript>().setGameOver(true);
        gameOverCanvas.SetActive(true);

    // if scores better than previous, save that score and update Ui values 
        if(score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
            
        scoreUI.GetComponent<TMPro.TextMeshProUGUI>().text = "" + score;
        highScoreUI.GetComponent<TMPro.TextMeshProUGUI>().text = "" + highScore;
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
        if(randomEnemy >= 2) 
            newEnemy.transform.position += new Vector3(0, 1, 0);

        newEnemy.SetActive(true);
    }

// Timer for spawning enemy
    IEnumerator SpawnTimer() {
        shouldSpawn = false;
        yield return new WaitForSeconds((float)2);
        shouldSpawn = true;
    }

// Changes scene or reloads the current one
    public void ChangeScene(string scene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
