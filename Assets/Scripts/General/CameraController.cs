using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform transitionCube1;
    public Transform transitionCube2;
    


	//publics
	public bool topDownMovmentEnabled = true;
	//camera Speeds
	public float topDownSpeed = 1f;
	public float showcaseSpeed = 1f;
	public float transitionSpeed = 1f;

	public CameraStates DefaultMode = CameraStates.TopDown;

	public enum CameraStates{TopDown, Showcase, Transition, FirstPerson, FreeLook};
	//for transitions
	private Vector3 startPos;
	private Vector3 targetPos;
	private float startTime;
	private float journeyLength;

	private Vector3 targetDir;

	//first person target, showcase target...
	private Transform targetFP;
	private Transform targetSC;

	private CameraStates cState;
	private CameraStates pState;



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
					temp.x -= topDownSpeed;
					transform.position = temp;
				}
				else if(Input.mousePosition.y < 3)
				{
					Vector3 temp = transform.position;
					//temp.z -= 1;
					temp.y -= topDownSpeed;
					transform.position = temp;
				}
				else if(Input.mousePosition.x > Screen.width - 3)
				{
					Vector3 temp = transform.position;
					temp.x += topDownSpeed;
					transform.position = temp;
				}
				else if(Input.mousePosition.y > Screen.height - 3)
				{
					Vector3 temp = transform.position;
					temp.y += topDownSpeed;
					transform.position = temp;
				}

				if(Input.GetAxis("Mouse ScrollWheel") > 0.01)
				{
					Vector3 temp = transform.position;
					temp.z += topDownSpeed;
					transform.position = temp;
				}
				else if(Input.GetAxis("Mouse ScrollWheel") < -0.01)
				{
					Vector3 temp = transform.position;
					temp.z -= topDownSpeed;
					transform.position = temp;
				}
			}
			break;
		case CameraStates.Transition:
			{
				float distCovered = (Time.time - startTime) * transitionSpeed;
				float fracJourney = distCovered / journeyLength;
				transform.position = Vector3.Lerp(startPos, targetPos, fracJourney);

				//needs to be not exact
				if(distCovered == journeyLength)
				{
					cState = pState;
				}
			}
			break;
		}
	}
	
	public bool InitTrasition(Vector3 tPos, Vector3 tDir)
	{
		if(cState != CameraStates.Transition)
		{
			startPos = transform.position;
			targetPos = tPos;

			pState = cState;
			cState = CameraStates.Transition;

			startTime = Time.time;
			journeyLength = Vector3.Distance(startPos, targetPos);
			return true;
		}
		return false;
	}

    public void TestTransition(int cube)
    {
        switch(cube)
        {
            case 1:
                InitTrasition(new Vector3(transitionCube1.position.x, transitionCube1.position.y, Camera.main.transform.position.z), Vector3.zero);
            break;
            case 2:
                InitTrasition(new Vector3(transitionCube2.position.x, transitionCube2.position.y, Camera.main.transform.position.z), Vector3.zero);
            break;
        }
    }

	public bool ChangeState()
	{
		return true;
	}
}


