using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    //-----------------------
    
	//publics
	public bool topDownMovmentEnabled = true;
	//camera Speeds
	public float topDownSpeed = 1f;
	public float showcaseSpeed = 1f;
	public float transitionAcceleration = 1f;
    public float transitionMaxSpeed = 100f;
    public float transitionMinSpeed = 6f;


	public CameraStates DefaultMode = CameraStates.TopDown;

    public Bounds cameraBounds;

	public enum CameraStates{TopDown, Showcase, Transition, FirstPerson, FreeLook};
	//for transitions
	private Vector3 startPos;
	private Vector3 targetPos;
	private float startTime;
    private float prevTime;
	private float journeyLength;
    private float totalDistCovered;
    public float tSpeed;

	private Vector3 targetDir;

	//first person target, showcase target...
	private Transform targetFP;
	private Transform targetSC;

	public CameraStates cState; //public for debug
	private CameraStates pState;


    //CONSTS
    static public float EPSILON = 1.0f;


	// Use this for initialization
	void Start () {
        cState = DefaultMode;
        tSpeed = transitionMinSpeed;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		switch(cState)
		{
		case CameraStates.TopDown:
			if(topDownMovmentEnabled)
			{
				if(Input.mousePosition.x < 3)
				{
					Vector3 temp = transform.position;
                    temp.z += topDownSpeed * 0.5f;
                    temp.x -= topDownSpeed * 0.5f;
					transform.position = temp;
                    if(!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.x += topDownSpeed;
                        transform.position = temp;
                    }
				}
				if(Input.mousePosition.y < 3)
				{
					Vector3 temp = transform.position;
                    temp.z -= topDownSpeed * 0.5f;
                    temp.x -= topDownSpeed * 0.5f;
					transform.position = temp;
                    if (!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.z += topDownSpeed;
                        transform.position = temp;
                    }
				}
				if(Input.mousePosition.x > Screen.width - 3)
				{
					Vector3 temp = transform.position;
                    temp.z -= topDownSpeed * 0.5f;
                    temp.x += topDownSpeed * 0.5f;
					transform.position = temp;
                    if (!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.x -= topDownSpeed;
                        transform.position = temp;
                    }
				}
				if(Input.mousePosition.y > Screen.height - 3)//up
				{
					Vector3 temp = transform.position;
					temp.z += topDownSpeed * 0.5f;
                    temp.x += topDownSpeed * 0.5f;
					transform.position = temp;
                    if (!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.z -= topDownSpeed;
                        transform.position = temp;
                    }
				}

				if(Input.GetAxis("Mouse ScrollWheel") > 0.01)
				{
					Vector3 temp = transform.position;
					temp.y -= topDownSpeed;
					transform.position = temp;
                    if (!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.y += topDownSpeed;
                        transform.position = temp;
                    }
				}
				if(Input.GetAxis("Mouse ScrollWheel") < -0.01)
				{
					Vector3 temp = transform.position;
					temp.y += topDownSpeed;
					transform.position = temp;
                    if (!cameraBounds.Contains(transform.position))//better way of doing this?
                    {
                        temp.y -= topDownSpeed;
                        transform.position = temp;
                    }
				}
			}
			break;
		case CameraStates.Transition:
			{
                float distCovered = (Time.time - prevTime) * tSpeed;

                //accelerate
                //need to change when acceleration happens
                //it should not be a fixed fraction
                //it shoudl be calculated based on the max/min/cur speed and the journey length and the acceleration will deff need to be used
                //idealy the camera will never be stuck at the min speed

                if (totalDistCovered < (journeyLength * 0.4))//this one is fine
                {
                    tSpeed += (tSpeed * transitionAcceleration);
                }
                else if (totalDistCovered > (journeyLength * 0.8))//this needs to be changed
                {
                    tSpeed -= (tSpeed * transitionAcceleration);
                }

                if (tSpeed > transitionMaxSpeed)
                {
                    tSpeed = transitionMaxSpeed;
                }
                else if (tSpeed < transitionMinSpeed)
                {
                    tSpeed = transitionMinSpeed;
                }
                
                totalDistCovered += distCovered;

                //move
                float fracJourney = totalDistCovered / journeyLength;
				transform.position = Vector3.Lerp(startPos, targetPos, fracJourney);
                prevTime = Time.time;



				//needs to be not exact
                Debug.Log(Mathf.Abs(totalDistCovered - journeyLength));
                if (Mathf.Abs(totalDistCovered - journeyLength) < EPSILON)
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
            prevTime = Time.time;
            totalDistCovered = 0;
            tSpeed = transitionMinSpeed;
			journeyLength = Vector3.Distance(startPos, targetPos);
			return true;
		}
		return false;
	}

    public bool InitTrasition(Vector3 tPos)
    {
        return InitTrasition(tPos, Vector3.zero);
    }

	public bool ChangeState()
	{
		return true;
	}
}


