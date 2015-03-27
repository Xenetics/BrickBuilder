using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {

	public GameObject[,] existingBlocks = new GameObject[5,7];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Build(GameObject block, int column)
	{
		int index = 0;

		for(int i = 0; i < 7; i++)
		{
			if(existingBlocks[column, i] == null)
			{
				index = i;
				break;
			}
		}

		if(index == 0)
		{
			block.transform.position = new Vector3((column - 2) * 4, 1, (index - 2) * -4);
			existingBlocks[column, 0] = GameObject.Instantiate(block);
		}
		else
		{
			for(int i = index; i > 0; i--)
			{
				existingBlocks[column, i - 1].transform.position = new Vector3((column - 2) * 4, 1, ((i - 2) * (-4)));
				existingBlocks[column, i] = existingBlocks[column, i - 1];
			}

			block.transform.position = new Vector3((column - 2) * 4, 1, (0 - 2) * -4);
			existingBlocks[column, 0] = GameObject.Instantiate(block);
		}
	}

	public bool ColFull(int column)
	{
		for(int i = 0; i < 7; i++)
		{
			if(existingBlocks[column, i] == null)
			{
				return false;
			}
		}
		return true;
	}
}
