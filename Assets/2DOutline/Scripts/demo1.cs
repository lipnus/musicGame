using UnityEngine;
using System.Collections;

public class demo1 : MonoBehaviour {

	public Vector3 vDemo1Pos = new Vector3 (0f, 1f, -10f);
	public Vector3 vDemo2Pos = new Vector3 (0f, 1f, -10f);
	public Vector3 vDemo3Pos = new Vector3 (0f, 1f, -10f);

	// Use this for initialization
	void Start () {
		//on start positionate iteself on demo1 view
		GoToPosition (1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//handle all the demo view here
	public void GoToPosition(int vIndex)
	{
		switch (vIndex) {
			//go to demo1 position
			case 1: 
				transform.position = vDemo1Pos;
			break;

			//go to demo2 position
			case 2: 
				transform.position = vDemo2Pos;
			break;

			//go to demo3 position
			case 3: 
				transform.position = vDemo3Pos;
			break;
		}
	}
}
