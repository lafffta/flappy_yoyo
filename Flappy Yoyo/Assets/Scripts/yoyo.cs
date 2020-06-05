using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// add to index.html: <a href="https://aidanbjelke.com"><h1>< Back to aidanbjelke.com</h1></a>
public class yoyo : MonoBehaviour
{
    public float gravity = 0.35f;
    public float tug = 9.2f;
    public float tugRate = 7.0f;
    float nextTugTime = 0f;
    public Vector3 gChange;
    public Vector3 tChange;
    bool tugging = false;
    public float gDelay = 2.5f;

    public GameManager gameManager;

    public GameObject stringObj;
    public AudioClip tugClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gDelay *= Time.fixedDeltaTime;
        gChange = new Vector3(0f, 0f, 0f);
        tChange = new Vector3(0f, 0f, 0f);
        gameManager=FindObjectsOfType<GameManager>()[0];
        // audioSource = FindObjectsOfType<AudioSource>()[0];
    }

    // Update is called once per frame
    private float hangtime = 0f;
    void Update()
    {
        if(!Pause.isPaused) {
            if(Time.time > nextTugTime) {
                 if(Input.anyKeyDown) {
                    hangtime = gDelay;
                    tugging = true;

                    audioSource.PlayOneShot(tugClip, 0.7f);
                    gChange = new Vector3(0f, 0f, 0f);
                    nextTugTime = Time.time + 1f / tugRate;
                 } else {
                    tugging = false;
                 }
            }

        }
    }

    private void FixedUpdate() {
        if(!Pause.isPaused) {
            Vector3 currChange;
            gChange += new Vector3(0f, -gravity*Time.fixedDeltaTime, 0f);
            if(tugging) {   // accel up from tug
                tChange = new Vector3(0f, tug*Time.fixedDeltaTime, 0f);
                currChange = tChange+gChange;
                transform.position = transform.position + currChange;
                stringObj.transform.position = stringObj.transform.position + currChange;
            } else {  // otherwise just apply gravity
                
                hangtime -= Time.fixedDeltaTime;
                if(hangtime <= 0) {
                    currChange = gChange;
                    transform.position = transform.position + currChange;
                    stringObj.transform.position = stringObj.transform.position + currChange;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Pause.isPaused = true;
        gameManager.deathUpdate();
    }
}
