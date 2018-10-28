using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

//	public Image image;
	public Text midText;

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




	//점수를 표시한다
	public void showText() {
		
	}
}
