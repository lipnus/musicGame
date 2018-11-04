using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieScene : MonoBehaviour {

	public Image spotLight;
	public Image note;
	public Text text;
	public Image black;
	
	// Use this for initialization
	void Start () {
		
		//검정 페이드아웃
		uiFadeOut(black,1f);
		
		//투명도초기화
		spotLight.GetComponent<CanvasRenderer>().SetAlpha(0f);
		note.GetComponent<CanvasRenderer>().SetAlpha(0f);
		text.GetComponent<CanvasRenderer>().SetAlpha(0f);

		//멘트설정
		DieText dieText = new DieText();
		text.text = dieText.getDieText();
		
		//코루틴시작
		StartCoroutine(dieScenario(0f));

	}

	//스포트라이트 먼저
	IEnumerator dieScenario(float delayTime) {
		
		uiFadeIn(spotLight,2f);
		yield return new WaitForSeconds(1.5f);
		uiFadeIn(note, 2f);
		uiFadeIn(text, 4f);
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

	//메인으로
	public void onClick_main() {
		SceneManager.LoadScene("MainScene");
	}

	//광고보고 부활(광고에서 콜백으로 여기 호출)
	public void respone() {
		GlobalScript.responeGame();
		SceneManager.LoadScene( GlobalScript.sceneName );
	}
}
