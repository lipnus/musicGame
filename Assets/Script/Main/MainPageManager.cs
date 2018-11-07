using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageManager : MonoBehaviour {
	
	//각각의 스마트폰 스크린
	public GameObject mobile;
	public GameObject main;
	public GameObject camera;
	public GameObject info;
	public GameObject logo;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		mobile.transform.Translate(Vector3.up * 0.06f);
	}

	//카메라메뉴
	public void onClick_camera() {
		main.active = false;
		camera.active = true;
		StartCoroutine("ReturnMainScreen", 4);
	}
	
	//정보메뉴
	public void onClick_info() {
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(0.3f, 0.9f);
		main.active = false;
		info.active = true;
		logo.active = false;
		StartCoroutine("ReturnMainScreenFadeIn", 3);
	}

	
	//3초후종료 + 페이드(정보 전용)
	IEnumerator ReturnMainScreenFadeIn(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		camera.active = false;
		info.active = false;
		main.active = true;
		logo.active = true;
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeIn(0.2f,0.9f);
		
	}
	
	//3초후 종료
	IEnumerator ReturnMainScreen(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		camera.active = false;
		info.active = false;
		main.active = true;
	}

	//카메라 쵤영버튼
	public void onClick_shot() {
		Debug.Log("찰칵");
		GameObject.Find("shot_img").GetComponent<Animator>().SetTrigger("shot_t");
		GameObject.Find("MainPageManager").GetComponent<MainSound>().playShutter();
	}
	
	//게임시작
	public void onClick_gamestart() {
		GlobalScript.startGame(); //게임초기화
		SceneManager.LoadScene("HomeScene");
	}
	
}
	
	
  
