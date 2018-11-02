using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {
	
	public void FadeIn(float delayTime) {
		
		Debug.Log("페이드인");
		Image thisImage = GetComponent<Image>();
		GetComponent<CanvasRenderer>().SetAlpha(1f);
		uiFadeOut(thisImage, delayTime);
	}

	public void FadeOut(float delayTime){
		Image thisImage = GetComponent<Image>();
		uiFadeIn(thisImage, delayTime);
	}
	

	//UI페이드인
	static void uiFadeIn(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(1f, t, false);//second param is the time
	}

	//UI페이드아웃
	static void uiFadeOut(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(1f);
		g.CrossFadeAlpha(0f, t, false);
	}
}
