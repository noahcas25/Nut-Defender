using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
     private GameObject gameController;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") || other.CompareTag("FlyingEnemy")) {
            gameController.GetComponent<GameControllerScript>().GameOver();
        }
    }
}
