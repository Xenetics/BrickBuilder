using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public float Speed = 10.0f;
    public float correctionAmount = 0.5f;
    public float correctionThreshold = 0.8f;
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
        if (body.velocity.z < correctionThreshold)
        {
            Debug.Log("Corrected direction");
            Vector3 temp = body.velocity;
            if( temp.z > 0)
            {
                temp.z += correctionAmount;
            }
            else
            {
                temp.z -= correctionAmount;
            }           
            body.velocity = temp;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(body.velocity.x > Speed * 0.8f)
        {

        }
    }
}
