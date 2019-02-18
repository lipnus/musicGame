 
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {

	public Image blackImg;
	
	public void FadeIn(float delayTime) {
		blackImg.color= new Color (1f, 1f, 1f, 1f);
		uiFadeOut(blackImg, delayTime, 1f);
	}

	public void FadeOut(float delayTime){
		blackImg.color = new Color (1f, 1f, 1f, 1f);
		uiFadeIn(blackImg, delayTime, 1f);
	}
	
	public void FadeIn(float delayTime, float opacity) {
		blackImg.color = new Color (1f, 1f, 1f, 1f);
		uiFadeOut(blackImg, delayTime, opacity);
	}

	public void FadeOut(float delayTime, float opacity){
		blackImg.color = new Color (1f, 1f, 1f, 1f);
		uiFadeIn(blackImg, delayTime, opacity);
	}

	public void aa() {
		blackImg.color = new Color (1f, 1f, 1f, 1f);
	}
	
	
	
	//UI페이드인
	void uiFadeIn(Graphic g, float t, float opacity){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(opacity, t, false);//second param is the time
	}

	//UI페이드아웃
	void uiFadeOut(Graphic g, float t, float opacity){
		g.GetComponent<CanvasRenderer>().SetAlpha(opacity);
		g.CrossFadeAlpha(0f, t, false);
	}
}
