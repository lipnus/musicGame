using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {

	public float userSpeed;
	public GameObject cloud;
	public GameObject user;
	public GameObject stand_light_on;
	public GameObject stand_light_off;

	public GameObject dark1;
	public GameObject dark2;


	public AudioSource switchSound;
	public AudioSource tickingSound;
	public AudioSource blanketSound;


	void Update() {
		cloud.transform.Translate(Vector3.left * 0.6f * Time.deltaTime);
	}
	
	// Use this for initialization
	void Start () {
		tickingSound.Play();
		StartCoroutine("Scenario", 5);
	}

	IEnumerator Scenario(float delayTime) { 
		yield return new WaitForSeconds(1);
		GameObject.Find("uiManager").GetComponent<UIManager>().showText("잠이 오지 않아..");
		yield return new WaitForSeconds(2);

		//불켜기
		tickingSound.Stop();
		stand_light_on.SetActive(true);
		stand_light_off.SetActive(false);
		dark1.SetActive(false);
		dark2.SetActive(true);
		switchSound.Play();
		GameObject.Find("uiManager").GetComponent<UIManager>().showText("공허한 마음을 달래줄 것은 오직 음악..");
		yield return new WaitForSeconds(1);
		
		//일어남
		blanketSound.Play();
		GameObject.Find("Bed").GetComponent<Animator>().SetTrigger("out_t");
		user.SetActive(true);
		
		//걷기시작
		user.GetComponent<Animator>().SetTrigger("walking_t");
		StartCoroutine("walking", 0);
		
		yield return new WaitForSeconds(6);
//		SceneManager.LoadScene("CityScene");		
		SceneManager.LoadScene("TutorialScene");		

	}
	
	
	//걷기루틴
	IEnumerator walking(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		user.transform.Translate(Vector3.right * userSpeed);
		StartCoroutine("walking", 0.01f);
	}
	 
}
