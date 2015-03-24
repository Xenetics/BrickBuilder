using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	
	private static InputManager instance = null;
	public static InputManager Instance { get { return instance; } }
	private AstarPath pathController;
	
	public List<GameObject> selectedUnits;

	public Rect selection = new Rect(0,0,0,0);
	private Vector3 startClick = Vector3.one;
	
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
		pathController = GameObject.FindGameObjectWithTag("PathController").GetComponent<AstarPath>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) {
			startClick = Input.mousePosition;
			/*
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, 1000))
			{
				if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Units"))
				{
					selectedUnit = hit.transform.gameObject;
					selectedUnit.GetComponent<Selector>().Select();
					pathScript = selectedUnit.GetComponent<AStarMove>();
				}
				else if()
				{
				}
				else
				{
					if(selectedUnit != null)
					{
						selectedUnit.GetComponent<Selector>().Deselect();
						selectedUnit = null;
					}
				}
			}
			*/
		} 
		else if (Input.GetMouseButtonUp (0)) 
		{
			foreach(GameObject g in selectedUnits)
			{
				g.GetComponent<Selector>().Deselect();
			}

			selectedUnits.Clear();

			if(selection.width < 25 || selection.height < 25)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if(Physics.Raycast(ray, out hit, 1000))
				{
					if(hit.transform.gameObject.tag == "Unit" || hit.transform.gameObject.layer == LayerMask.NameToLayer("Buildings"))
					{
						selectedUnits.Add(hit.transform.gameObject);
						hit.transform.gameObject.GetComponent<Selector>().Select();
					}
				}
			}
			else
			{
				if(selection.width < 0)
				{
					selection.x += selection.width;
					selection.width = - selection.width;
				}
				if(selection.height < 0)
				{
					selection.y += selection.height;
					selection.height = -selection.height;
				}

				startClick = -Vector3.one;

				foreach(GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
				{
					Vector3 unitCamPos = Camera.main.WorldToScreenPoint(g.transform.position);
					unitCamPos.y = InvertScreenY(unitCamPos.y);
					if(selection.Contains(unitCamPos))
					{
						selectedUnits.Add(g);
						g.GetComponent<Selector>().Select();
					}
					else
					{
						g.GetComponent<Selector>().Deselect();
					}
				}
			}
		}

		if (Input.GetMouseButton (0)) 
		{
			selection = new Rect(startClick.x, InvertScreenY(startClick.y), Input.mousePosition.x - startClick.x, InvertScreenY(Input.mousePosition.y) - InvertScreenY(startClick.y));
		}

		if(Input.GetMouseButtonDown(1))
		{
			if(selectedUnits.Count != 0)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					
				if(Physics.Raycast(ray, out hit, 1000))
				{
					if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
					{
						foreach(GameObject g in selectedUnits)
						{
							AStarMove unitPath = g.GetComponent<AStarMove>();
							unitPath.targetPosition = hit.point;
							unitPath.newPath();
						}
						/*
						marker.transform.position = hit.point + new Vector3(0f, 0.39f, 0f);
						marker.GetComponent<MarkerEffect>().Reset();
						marker.SetActive(true);
						*/
					}
					else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Resources"))
					{
						foreach(GameObject g in selectedUnits)
						{
							g.GetComponent<WorkerScript>().SetWork(hit.transform.gameObject);
						}
					}
				}
			}
		}
	}

	public float InvertScreenY(float y)
	{
		return Screen.height - y;
	}
}
