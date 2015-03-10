using UnityEngine;
using System.Collections;

public class WorkerScript : MonoBehaviour {

	public GameObject homeBase;
	public GameObject work;

	private bool working;

	private float workDuration;
	private float workTimer;

	private AStarMove pathScript;

	// Use this for initialization
	void Start () {
		homeBase = GameObject.FindGameObjectWithTag("Base");
		workDuration = 3.0f;
		workTimer = 0.0f;
		pathScript = GetComponent<AStarMove>();
	}
	
	// Update is called once per frame
	void Update () {
		if(work != null)
		{
			if (workTimer >= workDuration && working)
			{
				working = false;
				pathScript.targetPosition = homeBase.transform.position;
				pathScript.newPath();
			}

			//Debug.Log(Vector3.Distance (this.transform.position, homeBase.transform.position));
			//Debug.Log (workTimer);
			if ((Vector3.Distance (this.transform.position, homeBase.transform.position) < 0.9) && workTimer != 0)
			{
				workTimer = 0.0f;
				pathScript.targetPosition = work.transform.position;
				pathScript.newPath();
			}
			//Debug.Log (workTimer);

			if ((Vector3.Distance (this.transform.position, work.transform.position) < 0.5) && workTimer == 0)
			{
				pathScript.stopPath();
				working = true;
			}

			if(working)
			{
				//Debug.Log (Time.deltaTime);
				workTimer += Time.deltaTime;
			}
		}
	}

	public void SetWork(GameObject g)
	{
		Debug.Log ("Work Set");
		work = g;
		workTimer = 0.0f;
		pathScript.targetPosition = work.transform.position;
		pathScript.newPath();
	}
}
