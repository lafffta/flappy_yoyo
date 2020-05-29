using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyo : MonoBehaviour
{
    public float gravity = 0.35f;
    public float tug = 9f;
    public float tugRate = 6.0f;
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
    void Update()
    {
        if(!Pause.isPaused) {
            if(Time.time > nextTugTime) {
                tugging = false;
                if(Input.GetMouseButton(0)) {
                    tugging = true;
                    nextTugTime = Time.time + 1f / tugRate;
                    gChange = new Vector3(0f, 0f, 0f);
                    audioSource.PlayOneShot(tugClip, 0.7f);
                }
            }
        }
    }

    private float hangtime = 0f;
    private void FixedUpdate() {
        if(!Pause.isPaused) {
            Vector3 currChange;
            gChange += new Vector3(0f, -gravity*Time.fixedDeltaTime, 0f);
            tChange = new Vector3(0f, tug*Time.fixedDeltaTime, 0f);

            if(tugging) {   // accel up from tug
                hangtime = gDelay;
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
