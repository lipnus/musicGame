using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GuideObject : MonoBehaviour {
	
	public GuideType guideType;
	public GameObject fieldManager;
	public GameObject guideManager;
	
	public GameObject blackBackground;
	public GameObject midText;
	public GameObject sub;

	public User user;
	public SoundManager soundManager;
	
	
	public enum GuideType {
		FirstNote, Jump, Cat1, Cat2, Subway, End, textEnd
	}

	
	private void OnTriggerEnter2D(Collider2D col) {
		guideManager.GetComponent<GuideManager>().enrollGuideObj(gameObject); //가이드매니저에 이 객체를 전달
		startGuide();
	}
	
	public void startGuide() {
		if (guideType == GuideType.FirstNote) firstNote();
		else if (guideType == GuideType.Jump) jump();
		else if (guideType == GuideType.Cat1) cat1();
		else if (guideType == GuideType.Cat2) cat2();
		else if (guideType == GuideType.Subway) subway();
		else if (guideType == GuideType.End) endGuide();
		else if (guideType == GuideType.textEnd) textEnd();
	}



	//처음 음표를 만났을 때
	void firstNote() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "음표를 모아 아이템을 구입하세요";
	}
	
	//점프 설명
	void jump() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "화면을 터치하여 점프하세요";
	}
	
	//고양이를 만났을 때(초성)
	void cat1() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "야~옹\n고양이를 터치해 퀴즈를 확인하세요!";
		
		user.startShowIcon(0); //야옹아이콘
		soundManager.catPlay();
	}
	
	//고양이를 만났을 때(3지선다)
	void cat2() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "또다시 야~옹\n고양이를 터치하세요!";
		
		user.startShowIcon(0); //야옹아이콘
		soundManager.catPlay();
	}

	
	//지하철 발견
	void subway() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "지하철역에 도착하면 다음스테이지로 이동합니다!";
		
		user.startShowIcon(3); //지하철아이콘
	}
	
	
	//튜토리얼 완료
	void endGuide() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		midText.GetComponent<Text>().text = "튜토리얼 스테이지를 완료하였습니다! \n 플레이해주셔서 감사합니다.";
		midText.GetComponent<Animator>().SetBool("showText", true);
		
		GlobalScript.resetGame();
	}

	//퀴즈에서 가이드텍스트 끝
	void textEnd() {
		GlobalScript.endGuide();
	}
}
