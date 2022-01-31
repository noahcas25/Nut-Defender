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

// UI elememts
    [SerializeField]
    private GameObject highScoreUI, scoreUI, gameOverCanvas;

    private int enemiesCount;
    private int randomEnemy;
    private int score = 0;
    private int highScore = 0;
    private float spawnTime = 2;
    private bool shouldSpawn = true;
    private GameObject inGameScoreUi;
    private GameObjectPool enemyPool;

    void Start() {
        Application.targetFrameRate = 60;
        enemiesCount = enemies.transform.childCount;
        inGameScoreUi = GameObject.FindWithTag("ScoreUI");
        enemyPool = GetComponent<GameObjectPool>();
        enemyPool.AddToPool(10);
        StartCoroutine(SpawnTimer((float)5));
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
        GetComponent<AudioSource>().Play();
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

        if(score%10==0 && spawnTime > 1) {
            spawnTime -= (float)0.1;
        }

        score += value;
        inGameScoreUi.GetComponent<TMPro.TextMeshProUGUI>().text = "" + score;
    }

// Spawns random enemy and positions them at the target
    private void SpawnEnemy() { 
        StartCoroutine(SpawnTimer(spawnTime));

        GameObject newEnemy = enemyPool.Get();
        newEnemy.transform.position = markers.transform.GetChild(Random.Range(0, 8)).position;
        newEnemy.transform.LookAt(nutTreasure.transform, Vector3.up);
        newEnemy.SetActive(true);

        if(newEnemy.CompareTag("FlyingEnemy")) {
            newEnemy.transform.position += new Vector3(0, 1, 0);
        }
    }

    public void Exit() {
        Application.Quit();
    }

// Timer for spawning enemy
    IEnumerator SpawnTimer(float timer) {
        shouldSpawn = false;
        yield return new WaitForSeconds(timer);
        shouldSpawn = true;
    }

// Changes scene or reloads the current one
    public void ChangeScene(string scene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
