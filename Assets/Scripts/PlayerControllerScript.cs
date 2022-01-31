using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
// Variables
    [SerializeField]
    private GameObject gameController;

    private bool gameOver;
    private Animator anim;
    private AudioSource audioSource;

// Start is called before the first frame update
    void Start() {
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

// Function responsible for positioning player based on keystroke
    public void Move(string direction) {    

        if(!gameOver) {
            switch(direction) {
                case "Up":
                    if(transform.position.z < 0.4 && Mathf.Abs(transform.position.x) > 1) {
                        transform.position += new Vector3(0, 0, (float)2.75);
                        Handler(0);
                    }
                break;
                case "Down":
                    if(transform.position.z > -5 && Mathf.Abs(transform.position.x) > 1) {
                        transform.position -= new Vector3(0, 0, (float)2.75);
                        Handler(180);
                    }
                break;
                case "Left":
                    if(transform.position.x > -2.5 && !(transform.position.z < -1 && transform.position.z > -3)) {
                        transform.position -= new Vector3((float)2, 0, 0);
                        Handler(-90);
                    }
                break;
                case "Right":
                    if(transform.position.x < 1.3 && !(transform.position.z < -1 && transform.position.z > -3)) {
                        transform.position += new Vector3((float)2, 0, 0);
                        Handler(90);
                    }
                break;
        }}
    }

// Function to handle player rotations and animation call
    private void Handler(int value) {
        transform.rotation = Quaternion.Euler(0, value, 0);
        anim.Play("SSquirrel_Jump_Anim");
    }

// Setter for GameOver variable
    public void setGameOver(bool value) {
       gameOver = value;
    }

// On collision with enemy, return them back to pool, playing audio and animation and improve score
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") || other.CompareTag("FlyingEnemy")) {
            anim.Play("SquirrelAttack");
            audioSource.Play();
            gameController.GetComponent<GameObjectPool>().ReturnToPool(other.gameObject);
            gameController.GetComponent<GameControllerScript>().IncrementScore(1);
        }
    }
}
