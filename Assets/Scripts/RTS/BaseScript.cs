using UnityEngine;
using System.Collections.Generic;

public class BaseScript : MonoBehaviour {

	private List<GameObject> buildQueue = new List<GameObject>();

	public GameObject worker;
	public float workerBuildTime;

	public float timeRemaining;

	public GameObject selected;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Check for resources once implemented
		if(selected.activeSelf)
		{
			if (Input.GetKeyUp (KeyCode.Alpha1))
			{
				buildQueue.Add(worker);
				if(buildQueue.Count == 1)
				{
					timeRemaining = workerBuildTime;
				}
			}
		}

		if (timeRemaining > 0) 
		{
			timeRemaining -= Time.deltaTime;
		}

		if(timeRemaining <= 0 && buildQueue.Count > 0)
		{
			buildQueue.RemoveAt(0);
			GameObject.Instantiate(worker);
			if(buildQueue.Count > 0)
			{
				timeRemaining = workerBuildTime;
			}
		}
	}
}
