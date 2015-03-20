using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    [SerializeField]
    private bool rotateX = false;
    [SerializeField]
    private bool rotateY = false;
    [SerializeField]
    private bool rotateZ = false;

	void Start () 
    {
	    
	}
	
	void Update () 
    {
        if (rotateX)
        {
            entity.transform.Rotate(Vector3.right);
        }

        if (rotateY)
        {
            entity.transform.Rotate(Vector3.up);
        }

        if (rotateZ)
        {
            entity.transform.Rotate(Vector3.forward);
        }
	}
}
