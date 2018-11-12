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
		FirstNote
		
	}

	
	private void OnTriggerEnter2D(Collider2D col) {
		guideManager.GetComponent<GuideManager>().guideObj = gameObject;
		startGuide();
	}
	
	public void startGuide() {
		if (guideType == GuideType.FirstNote) firstNote();
	}



	//처음 음표를 만났을 때
	void firstNote() {
		sub.active = true;
		fieldManager.GetComponent<TutorialFieldManager>().pauseMove();
		blackBackground.GetComponent<Animator>().SetTrigger("fadein_t");
		midText.GetComponent<Animator>().SetBool("showText", true);
		midText.GetComponent<Text>().text = "음표를 획득하여 아이템을 구입하세요";
		Debug.Log("음표설명");
	}
}
