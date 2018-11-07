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
	public GameObject shop;
	
	public GameObject logo;
	private const float BACK_OPPACITY=0.9f;

	//카메라메뉴
	public void onClick_camera() {
		main.active = false;
		camera.active = true;
		StartCoroutine("ReturnMainScreen", 4);
	}

	//정보메뉴
	public void onClick_info() {
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(0.3f, BACK_OPPACITY);
		main.active = false;
		info.active = true;
		logo.active = false;
		StartCoroutine("ReturnMainScreenFadeIn", 3);
	}

	//쇼핑메뉴
	public void onClick_shop() {
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(0.3f, BACK_OPPACITY);
		main.active = false;
		shop.active = true;
		logo.active = false;
	}

	//뒤로버튼
	public void onClick_back() {
		StartCoroutine(ReturnMainScreen(0f));
	}
	
	//뒤로버튼
	public void onClick_back_fade() {
		StartCoroutine(ReturnMainScreenFadeIn(0f));
	}


	//페이드인 하면서 메인으로 돌아가기
	IEnumerator ReturnMainScreenFadeIn(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		camera.active = false;
		info.active = false;
		shop.active = false;
		logo.active = true;
		
		main.active = true;
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeIn(0.2f,BACK_OPPACITY);
		
	}
	
	//메인으로 돌아가기
	IEnumerator ReturnMainScreen(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		camera.active = false;
		info.active = false;
		shop.active = false;

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
	
	
  
