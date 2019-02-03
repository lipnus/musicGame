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
	public GameObject message_page; //메시지
	public GameObject rankingText;

	public ConnectServer ConnectServer;

	public GameObject messageCircle;

	public AudioSource backgroundMusic;

	public SoundManager2 soundManager;
	private const float BACK_OPPACITY=0.7f;

	void Start() {
	
//		Utils.resetGame();
		Utils.firstGift();
		Utils.setScore(100);
		synchroUserInfo();

	}

	//서버와 유저데이터를 동기화
	public void synchroUserInfo() {
		
		Debug.Log("현제닉네임: " + Utils.getNickname());
		
		if (Utils.getNickname().Equals("empty_nickname")) {
			Debug.Log("유저정보 다운로드 시도");
			ConnectServer.downloadUserInfo( Utils.getUUID() );
		}else {
			Debug.Log("유저정보 업로드");
			ConnectServer.uploadUserInfo( Utils.getUserInfo() );
		}
		
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
	
	
	//메시지
	public void onClick_message() {
		soundManager.playSound(1);
		message_page.active = true;
		start_page.active = false;
		messageCircle.active = false;

	}
	
	//랭킹 페이지
	public void onClick_ranking() {
		soundManager.playSound(1);
		rankingText.GetComponent<Animator>().SetTrigger("show_t");
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
		message_page.active = false;
		
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
		message_page.active = false;

	}

	//카메라 쵤영버튼
	public void onClick_shot() {
		GameObject.Find("shot_img").GetComponent<Animator>().SetTrigger("shot_t");
		soundManager.playSound(0);//셔터소리
	}
	
	//게임시작
	public void onClick_gamestart() {
		soundManager.playSound(1); //클릭소리
		Utils.startGame(); //게임초기화
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(1f, 1f);
		StartCoroutine(bgMusicFadeOut(0.1f));
		StartCoroutine(startGame(1.5f));
		
	}
	
	IEnumerator bgMusicFadeOut(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		backgroundMusic.volume-= 0.1f;
		StartCoroutine(bgMusicFadeOut(0.1f));
	}

	IEnumerator startGame(float delayTime) {
		yield return new WaitForSeconds(delayTime);

		if (Utils.getNickname().Equals("empty_nickname")) {
			SceneManager.LoadScene("NicknameScene");
		}
		else {
			SceneManager.LoadScene("HomeScene");		
		}
	}
}

	
  
