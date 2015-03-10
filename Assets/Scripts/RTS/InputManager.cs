using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	private static InputManager instance = null;
	public static InputManager Instance { get { return instance; } }
	
	private AStarMove pathScript;
	public GameObject selectedUnit;
	
	RaycastHit hit; 
	
	void Awake () {
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;        
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		//pathScript = player.GetComponent<AStarMove>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, 1000))
			{
				if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Units"))
				{
					selectedUnit = hit.transform.gameObject;
					pathScript = selectedUnit.GetComponent<AStarMove>();
				}
			}
		}
		if(Input.GetMouseButtonDown(1))
		{
			if(selectedUnit != null)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					
				if(Physics.Raycast(ray, out hit, 1000))
				{
					if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
					{
						pathScript.targetPosition = hit.point;
						pathScript.newPath();
						/*
						marker.transform.position = hit.point + new Vector3(0f, 0.39f, 0f);
						marker.GetComponent<MarkerEffect>().Reset();
						marker.SetActive(true);
						*/
					}
					else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Resources"))
					{
						selectedUnit.GetComponent<WorkerScript>().SetWork(hit.transform.gameObject);
					}
				}
			}
		}
	}
}
