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
	public GameObject ranking_page; //랭킹 

	public ConnectServer connectServer;
	public AudioSource backgroundMusic;
	public SoundManager2 soundManager;
	public MessageManager messageManager;
	public RankingManager rankingManager;
	
	private const float BACK_OPPACITY=0.7f;

	
	void Start() {
	
//		PlayerPrefs.DeleteAll();
//		Debug.Log("닉네임: " + Utils.getNickname());
		Utils.firstGift();
		Utils.setPoint(1);
		downloadMessage();
		
	}

 

	public void downloadMessage() {
//		messageManager.downloadMessage();
		messageManager.exhb_setMessage();
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
	
	//메시지 페이지
	public void onClick_message() {
		soundManager.playSound(1);
		message_page.active = true;
		start_page.active = false;
		messageManager.updateLastReadNum(); //가장 마지막으로 읽은 메시지번호 갱신
	}
	
	//랭킹 페이지
	public void onClick_ranking() {
		soundManager.playSound(1);
		ranking_page.active = true;
		start_page.active = false;
		rankingManager.initRank();
//		rankingText.GetComponent<Animator>().SetTrigger("show_t");
	}

	
	//뒤로버튼
	public void onClick_back() {
		soundManager.playSound(1); //클릭소리
		StartCoroutine(ReturnMainScreen(0f));
	}
	

	//페이드인 하면서 메인으로 돌아가기
	IEnumerator ReturnMainScreen(float delayTime) {
		
		yield return new WaitForSeconds(delayTime);
		
		camera_page.active = false;
		info_page.active = false;
		shop_page.active = false;
		message_page.active = false;
		ranking_page.active = false;

		start_page.active = true;
//		messageManager.downloadMessage(); //메시지 업데이트
		
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeIn(0.2f,BACK_OPPACITY);
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

		SceneManager.LoadScene("NicknameScene");

//		if (Utils.getNickname().Equals("empty_nickname")) {
//			SceneManager.LoadScene("NicknameScene");
//		}
//		else {
//			SceneManager.LoadScene("HomeScene");		
//		}
	}
	
}

	
  
