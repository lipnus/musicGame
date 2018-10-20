using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour {

	public float userSpeed;
	public GameObject user;
	public GameObject stand_light_on;
	public GameObject stand_light_off;

	public GameObject dark1;
	public GameObject dark2;

	public AudioSource switchSound;

	

	// Use this for initialization
	void Start () {
		
		StartCoroutine("Scenario", 3);
	}
	
	
	IEnumerator Scenario(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		
		Debug.Log("불켜기");
		stand_light_on.SetActive(true);
		stand_light_off.SetActive(false);
		dark1.SetActive(false);
		dark2.SetActive(true);
		switchSound.Play();
		
		yield return new WaitForSeconds(2);

		Debug.Log("깨어남");
		GameObject.Find("Bed").GetComponent<Animator>().SetTrigger("out_t");
		user.SetActive(true);
		
		//걷기시작
		user.GetComponent<Animator>().SetTrigger("walking_t");
		StartCoroutine("walking", 1);
	}
	
	
	//걷기
	IEnumerator walking(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
	
		user.transform.Translate(Vector3.right * userSpeed);
		StartCoroutine("walking", 0.01f);
	}
	 
}
