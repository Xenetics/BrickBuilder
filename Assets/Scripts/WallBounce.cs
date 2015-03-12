using UnityEngine;
using System.Collections;

public class WallBounce : MonoBehaviour {
    public bool x;

    void OnTriggerEnter(Collider other)
    {
        Vector3 temp = other.GetComponent<Rigidbody>().velocity;
        if(x == true)
        {
            temp.x *= -1;
        }
        else
        {
            temp.z *= -1;
        }
        other.GetComponent<Rigidbody>().velocity = temp;
    }
}
