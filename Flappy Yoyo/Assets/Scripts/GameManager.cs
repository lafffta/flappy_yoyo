using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Pause
{
     public static bool isPaused = true;
}

public class GameManager : MonoBehaviour
{
    public float spawnTime = 2.5f;
    public cat catPrefab;
    private float currentGameTime = 2.5f;

    public int score = 0;
    public int best = 0;
    public Text scoreText;
    public Text pauseText;
    public Text pauseSubText;
    public Text bestText;

    public GameObject stringObj;
    public GameObject yoyo;

    public float resetTimer = 0.7f; // timer to adjust how long before player can restart
    private float initTime;
    public AudioSource audioSource;
    public AudioSource bgmSource;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = FindObjectsOfType<AudioSource>()[0];
        initTime = resetTimer;
        // pauseText.gameObject.SetActive(false);
        catPrefab.catChange = new Vector3(-catPrefab.catSpeed*Time.fixedDeltaTime, 0f, 0f);
    }

    private bool resetFlag = false;
    // Update is called once per frame
    void Update()
    {
        if(resetFlag) {
            resetFlag = false;

            resetTimer = initTime;
            pauseText.gameObject.SetActive(false);
            score = 0;
            updateScoreText();
            currentGameTime = 2.0f;
            stringObj.transform.position = new Vector3(-1.8f,25.94f,0f);
            yoyo.transform.position = new Vector3(-1.8f,0f,0f);
            GameObject[] cats = GameObject.FindGameObjectsWithTag("cat");
            foreach(GameObject c in cats) {
                foreach (Transform child in c.transform) {
                    Destroy(child.gameObject);
                }
                Destroy(c);
                c.GetComponent<Renderer>().enabled = false;
            }
            yoyo.GetComponent<yoyo>().gChange = new Vector3(0f, -yoyo.GetComponent<yoyo>().gravity*Time.fixedDeltaTime, 0f);
            yoyo.GetComponent<yoyo>().tChange = new Vector3(0f, yoyo.GetComponent<yoyo>().tug*Time.fixedDeltaTime, 0f);
            Pause.isPaused = false;
        }

        if(resetTimer <= 0f) {
            if(Input.GetMouseButton(0)) { // reset game if player clicks after a short pause
                bgmSource.Play();
                resetFlag = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(!Pause.isPaused) {
            currentGameTime += Time.fixedDeltaTime;
            if(currentGameTime >= spawnTime) {
                currentGameTime = 0.0f;
                Instantiate(catPrefab, new Vector3(9.5f, Random.Range(-2.1f, -6.4f), 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
                // Instantiate(catPrefab, new Vector3(10.09f, -2.1f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
                // Instantiate(catPrefab, new Vector3(5.09f, -6.4f, 0f), new Quaternion(0f,0f,-0.7071068f,0.7071068f));
            }
        } else {
            resetTimer -= Time.fixedDeltaTime; // update paused timer
        }
    }

    public void updateScoreText()
    {
        scoreText.text = "Score: "+score.ToString();
        if(score > best) {
            best=score;
            bestText.text = "Best: "+best.ToString();
        }
    }
    public void deathUpdate()
    {
        bgmSource.Stop();
        audioSource.PlayOneShot(deathClip, 0.7f);
        pauseText.text = "DEAD";
        pauseSubText.text = "Click to restart";
        pauseText.gameObject.SetActive(true);
    }
}
