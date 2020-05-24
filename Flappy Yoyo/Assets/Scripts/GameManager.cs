using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float spawnTime = 2f;
    public cat catPrefab;
    private float currentGameTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        catPrefab.catChange = new Vector3(-catPrefab.catSpeed*Time.deltaTime, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        currentGameTime += Time.deltaTime;
        if(currentGameTime >= spawnTime) {
            currentGameTime = 0.0f;
            Instantiate(catPrefab, new Vector3(12.5f, Random.Range(-2.26f, -6.26f), 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
            // Instantiate(catPrefab, new Vector3(10.09f, -2.26f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
            // Instantiate(catPrefab, new Vector3(5.09f, -6.26f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
        }
    }
}
