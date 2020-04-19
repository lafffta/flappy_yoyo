using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyo : MonoBehaviour
{
    public float gravity = 2.0f;
    private Vector3 gChange;
    private Vector3 tChange;
    public GameObject stringObj;

    // Start is called before the first frame update
    void Start()
    {
        gChange = new Vector3(0f, -gravity*Time.deltaTime, 0f);
        tChange = new Vector3(0f, 4.0f*Time.deltaTime, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // GRAVITY
        transform.position = transform.position + gChange;
        stringObj.transform.position = stringObj.transform.position + gChange;

        // TUG
        if(Input.GetMouseButton(0)) {
            transform.position = transform.position + tChange;
            stringObj.transform.position = stringObj.transform.position + tChange;
        }
    }
}
