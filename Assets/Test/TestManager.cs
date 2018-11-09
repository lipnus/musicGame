using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GlobalScript.firstGift();
		GlobalScript.addMyItem(1818);
		
		Debug.Log("가지고있는가: " + GlobalScript.isHaveItem(1818));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
