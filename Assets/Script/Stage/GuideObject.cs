using System.Collections;
using System.Collections.Generic;
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
	
	
	public enum GuideType {
		FirstNote, Jump, Cat1
	}

	
	private void OnTriggerEnter2D(Collider2D col) {
		guideManager.GetComponent<GuideManager>().enrollGuideObj(gameObject); //가이드매니저에 이 객체를 전달
		startGuide();
	}
	
	public void startGuide() {
		if (guideType == GuideType.FirstNote) firstNote();
		else if (guideType == GuideType.Jump) jump();
		else if (guideType == GuideType.Cat1) cat1();
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
	
	//고양이를 만났을 때
	void cat1() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "야~옹\n고양이를 터치하세요!";
		
		GameObject.Find("cat_icon").GetComponent<Animator>().SetBool("cat_b", true);
		GameObject.Find("SoundManager").GetComponent<SoundManager>().catPlay();
	}
}
