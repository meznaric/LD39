using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnEnable()
    {
        if (this.transform.position.x > 0)
            speed *= -1;

        GetComponent<Rigidbody2D>().velocity = transform.right * -speed;
    }
}
