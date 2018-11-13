using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageManager : MonoBehaviour {

	//각 페이지 
//	public GameObject mobile;
	public GameObject start_page; //초기화면
	public GameObject camera_page; //카메라
	public GameObject info_page; //정보
	public GameObject shop_page; //쇼핑

	public SoundManager2 soundManager;
	private const float BACK_OPPACITY=0.7f;

	void Start() {
		GlobalScript.firstGift();
		GlobalScript.setScore(10000);
	}
	
	//카메라 페이지
	public void onClick_camera() {
		soundManager.playSound(1); //클릭소리
		start_page.active = false;
		camera_page.active = true;
	}

	//정보 페이지
	public void onClick_info() {
		soundManager.playSound(1);
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(0.3f, BACK_OPPACITY);
		start_page.active = false;
		info_page.active = true;
	}

	//쇼핑 페이지
	public void onClick_shop() {
		soundManager.playSound(1); //클릭소리
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(0.3f, BACK_OPPACITY);
		start_page.active = false;
		shop_page.active = true;
	}

	//뒤로버튼
	public void onClick_back() {
		soundManager.playSound(1); //클릭소리
		StartCoroutine(ReturnMainScreen(0f));
	}
	
	//뒤로버튼
	public void onClick_back_fade() {
		soundManager.playSound(1); //클릭소리
		StartCoroutine(ReturnMainScreenFadeIn(0f));
	}

	//페이드인 하면서 메인으로 돌아가기
	IEnumerator ReturnMainScreenFadeIn(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		camera_page.active = false;
		info_page.active = false;
		shop_page.active = false;
		
		start_page.active = true;
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeIn(0.2f,BACK_OPPACITY);
	}
	
	//메인으로 돌아가기
	IEnumerator ReturnMainScreen(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		camera_page.active = false;
		info_page.active = false;
		shop_page.active = false;
		start_page.active = true;
	}

	//카메라 쵤영버튼
	public void onClick_shot() {
		Debug.Log("찰칵");
		GameObject.Find("shot_img").GetComponent<Animator>().SetTrigger("shot_t");
		soundManager.playSound(0);//셔터소리
	}
	
	//게임시작
	public void onClick_gamestart() {
		soundManager.playSound(1); //클릭소리
		GlobalScript.startGame(); //게임초기화
		SceneManager.LoadScene("HomeScene");
	}	
}

	
  
