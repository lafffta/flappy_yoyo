using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Pause
{
     public static bool isPaused = false;
}

public class GameManager : MonoBehaviour
{
    public float spawnTime = 2f;
    public cat catPrefab;
    private float currentGameTime = 2.0f;

    public int score = 0;
    public int best = 0;
    public Text scoreText;
    public Text pauseText;
    public Text bestText;

    public GameObject stringObj;
    public GameObject yoyo;

    // Start is called before the first frame update
    void Start()
    {
        pauseText.gameObject.SetActive(false);
        catPrefab.catChange = new Vector3(-catPrefab.catSpeed*Time.deltaTime, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.isPaused) {
            currentGameTime += Time.deltaTime;
            if(currentGameTime >= spawnTime) {
                currentGameTime = 0.0f;
                Instantiate(catPrefab, new Vector3(12.5f, Random.Range(-2.26f, -6.26f), 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
                // Instantiate(catPrefab, new Vector3(10.09f, -2.26f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
                // Instantiate(catPrefab, new Vector3(5.09f, -6.26f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
            }
        } else {
            if(Input.GetMouseButton(0)) {
                pauseText.gameObject.SetActive(false);
                score = 0;
                updateScoreText();
                currentGameTime = 2.0f;
                // reset player and delete all cats here
                stringObj.transform.position = new Vector3(0f,25.94f,0f);
                yoyo.transform.position = new Vector3(0f,0f,0f);
                GameObject[] cats = GameObject.FindGameObjectsWithTag("cat");
                foreach(GameObject c in cats) {
                    foreach (Transform child in c.transform) {
                        Destroy(child.gameObject);
                    }
                    Destroy(c);
                    c.GetComponent<Renderer>().enabled = false;
                }
                Pause.isPaused = false;
            }
        }
    }

    public void updateScoreText()
    {
        scoreText.text = "Score: "+score.ToString();
    }
    public void deathUpdate()
    {
        pauseText.gameObject.SetActive(true);
        // update best score here
    }
}
