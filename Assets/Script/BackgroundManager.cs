using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject sky;
	public GameObject cloud;
	public GameObject building;
	public GameObject near;

	float userSpeed;

	// this for initialization
	void Start () {
        userSpeed = 2.5f;
		
	}
	
	// Update is called once per frame
	void Update () {
        sky.transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
		cloud.transform.Translate(Vector3.right * (userSpeed-0.1f) * Time.deltaTime);
		building.transform.Translate(Vector3.right * (userSpeed-1.3f) * Time.deltaTime);
        
		//near.transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
	}

    public void userSpeedControl(float percent){
        this.userSpeed *= percent;
    }
}
