using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{

    private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * Time.deltaTime * speed);
    }

    public void SetSpeed(int value) {
        this.speed = value;
    }
}
