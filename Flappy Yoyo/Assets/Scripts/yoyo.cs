using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyo : MonoBehaviour
{
    public float gravity = 0.35f;
    public float tug = 0.8f;
    public float tugRate = 5.0f;
    float nextTugTime = 0f;
    public Vector3 gChange;
    public Vector3 tChange;
    bool tugging = false;
    public float gDelay = 2.5f;

    public GameManager gameManager;

    public GameObject stringObj;
    // Start is called before the first frame update
    void Start()
    {
        gDelay *= Time.fixedDeltaTime;
        gChange = new Vector3(0f, 0f, 0f);
        tChange = new Vector3(0f, 0f, 0f);
        gameManager=FindObjectsOfType<GameManager>()[0];
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
                }
            }
        }
    }

    private float hangtime = 0f;
    private void FixedUpdate() {
        if(!Pause.isPaused) {
            // GRAVITY
            // transform.position = transform.position + gChange;
            // stringObj.transform.position = stringObj.transform.position + gChange;
            Vector3 currChange;
            if(tugging) {   // accel up from tug
                hangtime = gDelay;
                gChange += new Vector3(0f, -gravity*Time.fixedDeltaTime, 0f);
                tChange += new Vector3(0f, tug*Time.fixedDeltaTime, 0f);
                currChange = tChange;
                transform.position = transform.position + currChange;
                stringObj.transform.position = stringObj.transform.position + currChange;
            } else { // otherwise just apply gravity
                hangtime -= Time.fixedDeltaTime;
                if(hangtime <= 0) {
                    gChange += new Vector3(0f, -gravity*Time.fixedDeltaTime, 0f);
                    tChange = new Vector3(0f, tug*Time.fixedDeltaTime, 0f);
                    transform.position = transform.position + gChange;
                    stringObj.transform.position = stringObj.transform.position + gChange;
                } else {
                    
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
