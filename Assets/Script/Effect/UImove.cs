using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImove : MonoBehaviour {

	public Image phone;
	private int upCount = 0;
	private float rotateCount = 0;
	public float delay;
	public float speed;
	
	// Update is called once per frame
	void Start () {

		if (Application.loadedLevelName.Equals("MainScene")) {
			StartCoroutine(moveUp(delay, speed));
//			StartCoroutine(rotateCCW(0));
		}
		else if (Application.loadedLevelName.Equals("Quiz_initial")) {
			StartCoroutine(moveUp(delay, speed));
		}
		else if(Application.loadedLevelName.Equals("Quiz_choice")) {
			StartCoroutine(moveUp2(1f));
		}
	}



	IEnumerator moveUp(float delayTime, float speed) {
		yield return new WaitForSeconds(delayTime);

		upCount++;
		phone.transform.Translate(Vector3.up * speed);
		if(upCount<50) StartCoroutine(moveUp(delayTime, speed)); //0.005f
	}
	
	IEnumerator moveUp2(float delayTime) {
		yield return new WaitForSeconds(delayTime);

		upCount++;
		phone.transform.Translate(Vector3.up * 0.1f);
		if(upCount<50) StartCoroutine(moveUp2(0.005f)); //0.005f
	}


	IEnumerator rotateCCW(float delayTime) {
		yield return new WaitForSeconds(delayTime);

		rotateCount++;
		phone.transform.Rotate(0,0,-0.17f);
		if (rotateCount < 50) StartCoroutine(rotateCCW(0.005f));

	}
}
