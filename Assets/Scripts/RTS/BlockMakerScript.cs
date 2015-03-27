using UnityEngine;
using System.Collections.Generic;

public class BlockMakerScript : MonoBehaviour {

	private BlockManager blockManager;

	public GameObject[] blocks;
	public float blockBuildTime;
	public float timeRemaining;
	private GameObject currentlyBuilding;
	private int column;

	public List<GameObject> blockQueue;

	public GameObject selected;

	// Use this for initialization
	void Start () {
		blockManager = GameObject.FindGameObjectWithTag ("BlockManager").GetComponent<BlockManager> ();
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < 5; j++)
			{
				blockManager.Build(blocks[Random.Range(0, 5)], j);
			}
		}

		for(int i = 0; i < 10; i++)
		{
			blockQueue.Add(blocks[Random.Range(0, 5)]);
		}

		timeRemaining = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(selected.activeSelf)
		{
			if (Input.GetKeyUp (KeyCode.Alpha1) && timeRemaining <= 0 && !blockManager.ColFull(0))
			{
				currentlyBuilding = blockQueue[0];
				column = 0;
				blockQueue.RemoveAt(0);
				blockQueue.Add(blocks[Random.Range(0, 5)]);
				timeRemaining = blockBuildTime;
			}
			if (Input.GetKeyUp (KeyCode.Alpha2) && timeRemaining <= 0 && !blockManager.ColFull(1))
			{
				currentlyBuilding = blockQueue[0];
				column = 1;
				blockQueue.RemoveAt(0);
				blockQueue.Add(blocks[Random.Range(0, 5)]);
				timeRemaining = blockBuildTime;
			}
			if (Input.GetKeyUp (KeyCode.Alpha3) && timeRemaining <= 0 && !blockManager.ColFull(2))
			{
				currentlyBuilding = blockQueue[0];
				column = 2;
				blockQueue.RemoveAt(0);
				blockQueue.Add(blocks[Random.Range(0, 5)]);
				timeRemaining = blockBuildTime;
			}
			if (Input.GetKeyUp (KeyCode.Alpha4) && timeRemaining <= 0 && !blockManager.ColFull(3))
			{
				currentlyBuilding = blockQueue[0];
				column = 3;
				blockQueue.RemoveAt(0);
				blockQueue.Add(blocks[Random.Range(0, 5)]);
				timeRemaining = blockBuildTime;
			}
			if (Input.GetKeyUp (KeyCode.Alpha5) && timeRemaining <= 0 && !blockManager.ColFull(4))
			{
				currentlyBuilding = blockQueue[0];
				column = 4;
				blockQueue.RemoveAt(0);
				blockQueue.Add(blocks[Random.Range(0, 5)]);
				timeRemaining = blockBuildTime;
			}
		}

		if (timeRemaining > 0) 
		{
			timeRemaining -= Time.deltaTime;
		}
		
		if(timeRemaining <= 0 && currentlyBuilding != null)
		{
			blockManager.Build(currentlyBuilding, column);
			currentlyBuilding = null;
		}
	}
}
