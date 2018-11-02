﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

//	public Image image;
	public Text midText;
	
	//목숨
	public Image lifeBar;
	private const int LIFE_UNIT = 166; //목숨 한칸의 양
	private const int LIFE_MAX = 500; //최대 목숨의 양
	private int lifebarCount; //코루틴에서 라이프바 수정할때 쓰는 타이머
	
	//게임오버
	public Image userDieBackgroud;

	public void showText(string str) {
		midText.text = str;
		FadeIn(midText, 0.5f);
		StartCoroutine("stayText", 1.5f);		
	}
	
	public void showText_Long(string str) {
		midText.text = str;
		FadeIn(midText, 0.5f);
		StartCoroutine("stayText", 4f);		
	}

	IEnumerator stayText(float delayTime) {
		yield return new WaitForSeconds(delayTime); //표시시간
		FadeOut(midText, 0.5f);
	}
	
	public static void FadeIn(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(1f, t, false);//second param is the time
	}
	
	public static void FadeOut(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(1f);
		g.CrossFadeAlpha(0f, t, false);
	}
	
	
	
	//라이프바 한칸 내림
	//라이프바의 크기는 500이며 한칸의 크기는 166이다
	public void decreaseLifeBar() {
		lifebarCount = 0;
		StartCoroutine(modifyLifeBarSize(0, -1));
	}

	//라이프바 한칸 올림
	public void increaseLifeBar() {
		lifebarCount = 0;
		StartCoroutine(modifyLifeBarSize(0, 1));
	}
	
	
	IEnumerator modifyLifeBarSize(float delayTime, int val) { //val: +1, -1
		yield return new WaitForSeconds(delayTime);
		
		RectTransform rt = lifeBar.rectTransform;
		float nowWidth = rt.rect.width;
		float nowHeight = rt.rect.height;
		
		rt.sizeDelta = new Vector2 (nowWidth+(1f*val), nowHeight);

		lifebarCount++;
		if (lifebarCount < LIFE_UNIT) StartCoroutine(modifyLifeBarSize(0.005f, val));
	}

	//라이프바 값에 맞게 업데이트
	public void updateLifeBar() {
		RectTransform rt = lifeBar.rectTransform;
		float nowWidth = GlobalScript.getLife() * LIFE_UNIT;
		float nowHeight = rt.rect.height;
		rt.sizeDelta = new Vector2 (nowWidth, nowHeight);
	}

	//점수를 표시한다
	public void showText() {
		
	}


	//게임오벼 연출
	public void UIUserDie() {
		userDieBackgroud.gameObject.SetActive(true);
		FadeIn(userDieBackgroud, 4f);
		
		StartCoroutine(DieEvent(4));
	}

	IEnumerator DieEvent(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene("DieScene");

	}
}
