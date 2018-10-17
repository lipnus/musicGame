using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BackgroundManager : MonoBehaviour {

	public GameObject sky;
	public GameObject cloud;
	public GameObject building;
	public GameObject user;
	public GameObject near;

	float userSpeed;

	void Start () {
        userSpeed = 2.5f;
		user.transform.position = GlobalScript.userPosition;
		sky.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		sky.transform.Translate(Vector3.forward*10f);
	}
	
	void Update () {
        sky.transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
		cloud.transform.Translate(Vector3.right * (userSpeed-0.1f) * Time.deltaTime);
		building.transform.Translate(Vector3.right * (userSpeed-1.3f) * Time.deltaTime);
	}

    public void userSpeedControl(float percent){
        this.userSpeed *= percent;
    }

	public void catEffect() {

		//투시시점
		Camera.main.orthographic = false;
		GameObject.Find("Sky").transform.localScale += new Vector3(2f, 2f, 0);

		//카메라초점거리
		GameObject.Find("Main Camera").GetComponent<Camera>().focalLength = 6f;
		
		StartCoroutine("rotateCamera", 0.1);
	}
	
	
	//카메라시점변화
	IEnumerator rotateCamera(float delayTime) { 
		yield return new WaitForSeconds(delayTime);

		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		cam.transform.Rotate(Vector3.up * 0.15f); 
		cam.transform.Translate( Vector3.back * 0.005f);

		StartCoroutine("rotateCamera", 0.05f);
	}
}
