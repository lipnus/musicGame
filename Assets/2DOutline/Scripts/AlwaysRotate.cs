using UnityEngine;
using System.Collections;

public class AlwaysRotate : MonoBehaviour {

	private float RotateByAngle = 0f;
	public float vSpeed = 5f;

	// Use this for initialization
	void Start () {
		//get the rotatebyangle
		RotateByAngle = -Time.deltaTime*vSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//make it rotate
		Vector3 temp = transform.rotation.eulerAngles;
		temp.x = 0f;
		temp.y = 0f;
		temp.z += RotateByAngle;
		transform.rotation = Quaternion.Euler(temp);
	}
}
