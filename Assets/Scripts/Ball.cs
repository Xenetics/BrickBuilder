using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public float Speed = 10.0f;
    private Rigidbody body;
    public float damage = 20;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(Speed, 0, Speed);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        body.velocity = body.velocity.normalized * Speed;
	}

}
