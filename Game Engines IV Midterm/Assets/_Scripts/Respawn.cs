using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject currentSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject start = new GameObject("Start Spawn");
        currentSpawnPoint = start;
        currentSpawnPoint.transform.position = gameObject.transform.position + Vector3.up * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        gameObject.transform.position = currentSpawnPoint.transform.position;
        gameObject.GetComponent<PlayerController>().yDirection = Vector3.zero;
    }
}
