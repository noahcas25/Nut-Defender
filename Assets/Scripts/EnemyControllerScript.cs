using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * Time.deltaTime * (float)1.5);
    }
}
