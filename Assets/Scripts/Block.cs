using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public float maxHealth = 100;

    private float currHealth;
	// Use this for initialization
	void Start () {
        currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        //some sort of if ball clause
        if(collision.gameObject.GetComponent<Ball>() != null)
        {
            Debug.Log(collision.gameObject.GetComponent<Ball>().damage + " Damage BB");
            currHealth -= collision.gameObject.GetComponent<Ball>().damage;
            if(currHealth < 0)
            {
                Debug.Log("Boom!");
                Destroy(this.gameObject);
            }
        }
    }
}
