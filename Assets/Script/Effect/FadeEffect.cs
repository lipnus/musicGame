using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {

	public Image blackImg;
	
	public void FadeIn(float delayTime) {
		Debug.Log("fadeIn");
		blackImg.color = new Color (1f, 1f, 1f, 1f);
		uiFadeOut(blackImg, delayTime);
	}

	public void FadeOut(float delayTime){
		uiFadeIn(blackImg, delayTime);
	}
	
	//UI페이드인
	void uiFadeIn(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(1f, t, false);//second param is the time
	}

	//UI페이드아웃
	void uiFadeOut(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(1f);
		g.CrossFadeAlpha(0f, t, false);
	}
}
