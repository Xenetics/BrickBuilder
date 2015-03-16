using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public GameObject indicator;

	// Use this for initialization
	void Start () {
		indicator.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select()
	{
		indicator.SetActive (true);
	}

	public void Deselect()
	{
		indicator.SetActive (false);
	}
}
