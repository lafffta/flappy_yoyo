using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyo : MonoBehaviour
{
    public float gravity = 5.0f;
    public float tug = 10.0f;
    public float tugRate = 6.0f;
    float nextTugTime = 0f;
    private Vector3 gChange;
    private Vector3 tChange;
    bool tugging = false;

    public GameManager gameManager;

    public GameObject stringObj;
    // Start is called before the first frame update
    void Start()
    {
        gChange = new Vector3(0f, -gravity*Time.fixedDeltaTime, 0f);
        tChange = new Vector3(0f, tug*Time.fixedDeltaTime, 0f);
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
                }
            }
        }
    }

    private void FixedUpdate() {
        if(!Pause.isPaused) {
            // GRAVITY
            // transform.position = transform.position + gChange;
            // stringObj.transform.position = stringObj.transform.position + gChange;
            Vector3 currChange;
            if(tugging) {   // accel up from tug
                currChange = tChange + gChange;
                transform.position = transform.position + currChange;
                stringObj.transform.position = stringObj.transform.position + currChange;
            } else { // otherwise just apply gravity
                transform.position = transform.position + gChange;
                stringObj.transform.position = stringObj.transform.position + gChange;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Pause.isPaused = true;
        gameManager.deathUpdate();
    }
}
