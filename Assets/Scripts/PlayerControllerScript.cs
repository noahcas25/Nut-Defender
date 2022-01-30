using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject gameController;

    private bool gameOver;

    public void Move(string direction) {    

        if(!gameOver) {
            switch(direction) {
                case "Up":
                    if(transform.position.z < 0.4 && Mathf.Abs(transform.position.x) > 1) {
                        transform.position += new Vector3(0, 0, (float)2.75);
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                break;
                case "Down":
                    if(transform.position.z > -5 && Mathf.Abs(transform.position.x) > 1) {
                        transform.position -= new Vector3(0, 0, (float)2.75);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                break;
                case "Left":
                    if(transform.position.x > -2.5 && !(transform.position.z < -1 && transform.position.z > -3)) {
                        transform.position -= new Vector3((float)2, 0, 0);
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                break;
                case "Right":
                    if(transform.position.x < 1.3 && !(transform.position.z < -1 && transform.position.z > -3)) {
                        transform.position += new Vector3((float)2, 0, 0);
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                break;
        }}

        GetComponent<Animator>().Play("SSquirrel_Jump_Anim");
    }

    public void setGameOver(bool value) {
       gameOver = value;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")) {
        // Send to gameObject pool
            Destroy(other.gameObject, 0f);
            gameController.GetComponent<GameControllerScript>().IncrementScore(1);
        }
    }
}
