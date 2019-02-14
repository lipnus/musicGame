using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allWhite : MonoBehaviour {

	public GameObject obj;
	
	// Use this for initialization
	void Start () {
		SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();//Get the renderer via GetComponent or have it cached previously
			renderer.color = new Color(0f, 0f, 0f, 1f); // Set to opaque black
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
