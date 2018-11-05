using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneUp : MonoBehaviour {

	public Image phone;
	private int upCount = 0;
	
	// Update is called once per frame
	void Start () {
		phoneUpStart();
	}

	public void phoneUpStart() {
		StartCoroutine(phoneMoving(1f));
	}

	IEnumerator phoneMoving(float delayTime) {
		yield return new WaitForSeconds(delayTime);

		upCount++;
		phone.transform.Translate(Vector3.up * 0.1f);
		if(upCount<50) StartCoroutine(phoneMoving(0.005f));
	}
}
