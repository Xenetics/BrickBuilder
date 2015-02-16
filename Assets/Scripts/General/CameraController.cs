using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	//publics
	public bool topDownMovmentEnabled = true;
	//camera Speeds
	public float speedTopDown = 1f;
	public float speedShowcase = 1f;

	public CameraStates DefaultMode = CameraStates.TopDown;

	public enum CameraStates{TopDown, Showcase, Transition, FirstPerson};
	//for transitions
	private Vector3 targetPos;
	private Vector3 targetDir;
	//first person target, showcase target...
	private Transform targetFP;
	private Transform targetSC;

	private CameraStates cState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(cState)
		{
		case CameraStates.TopDown:
			if(topDownMovmentEnabled)
			{
				if(Input.mousePosition.x < 3)
				{
					Vector3 temp = transform.position;
					//temp.z -= 1;
					temp.x -= speedTopDown;
					transform.position = temp;
				}
				else if(Input.mousePosition.y < 3)
				{
					Vector3 temp = transform.position;
					//temp.z -= 1;
					temp.y -= speedTopDown;
					transform.position = temp;
				}
				else if(Input.mousePosition.x > Screen.width - 3)
				{
					Vector3 temp = transform.position;
					//temp.z -= 1;
					temp.x += speedTopDown;
					transform.position = temp;
				}
				else if(Input.mousePosition.y > Screen.height - 3)
				{
					Vector3 temp = transform.position;
					//temp.z -= 1;
					temp.y += speedTopDown;
					transform.position = temp;
				}

				if(Input.GetAxis("Mouse ScrollWheel") > 0.1)
				{
					Vector3 temp = transform.position;
					temp.z += speedTopDown;
					transform.position = temp;
				}
				else if(Input.GetAxis("Mouse ScrollWheel") < -0.1)
				{
					Vector3 temp = transform.position;
					temp.z -= speedTopDown;
					transform.position = temp;
				}
			}
			break;
		}
	}
}
