using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public float catSpeed = 2.5f;
    public Vector3 catChange;
    public yoyo player;
    private bool notScored = true;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // catChange = new Vector3(-catSpeed*Time.deltaTime, 0f, 0f); // now handled in game manager so it is constant for all cats
        player=FindObjectsOfType<yoyo>()[0];
        gameManager=FindObjectsOfType<GameManager>()[0];
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!Pause.isPaused) {
            transform.position += catChange;
            if(notScored) {
                if(transform.position.x < player.transform.position.x) {
                    gameManager.score += 1;
                    gameManager.updateScoreText();
                    notScored = false;
                }
            } else if(transform.position.x < -30f) {
                Destroy(gameObject);
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag.Equals("Player")) {
    //         Debug.Log("Player hit cat!\n");
    //     }
    // }
}
