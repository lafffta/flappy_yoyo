using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public float catSpeed = 1.0f;
    public Vector3 catChange;
    // Start is called before the first frame update
    void Start()
    {
        // catChange = new Vector3(-catSpeed*Time.deltaTime, 0f, 0f); // now handled in game manager so it is constant for all cats
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += catChange;
        if(transform.position.x < -30f) {
            Destroy(gameObject);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag.Equals("Player")) {
    //         Debug.Log("Player hit cat!\n");
    //     }
    // }
}
