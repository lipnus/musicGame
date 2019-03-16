using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DieManager : MonoBehaviour {

	public Image spotLight;
	public Text text;
	public Image black;
	public UnityAdsManager_Rewarded ad_reward;

	public GameObject rivivalButton;
	public GameObject pointButton;
	public GameObject normalButton;

	public SoundManager soundManager;

	public GameObject top;
	public GameObject bottom;
	public GameObject shoes;

	public FadeEffect fadeEffect;
	public Text PlayDataResultText;

	public GameObject bonusButtons;

	public GameObject canvas;
	
	

	
	//광고종료시에 얘를 호출
	public void initScene(BonusType bonusType) {
		
		//canvas 활성화
		canvas.active = true;
		
		//결과표시
		showPlayData();
		
		//시체표시
		showDeadBody();
		
		//검정 페이드아웃
		uiFadeOut(black,1f);
	
		
		//투명도초기화
		spotLight.GetComponent<CanvasRenderer>().SetAlpha(0f);
		text.GetComponent<CanvasRenderer>().SetAlpha(0f);

		//멘트설정
		DieText dieText = new DieText();
		text.text = dieText.getDieText();
		
		//코루틴시작
		StartCoroutine(dieScenario(bonusType));
	}
	
	
	
	//보상에 맞게 버튼 표시
	private void initBonusButton(BonusType bonusType) {

		if (bonusType == BonusType.Rivival) {
			rivivalButton.active = true;
			Utils.getPlayData().isRivival = true;
		}else if (bonusType == BonusType.PointBonus) {
			pointButton.active = true;	
		}else {
			normalButton.active = true;
		}
	}

	//스포트라이트 먼저
	IEnumerator dieScenario(BonusType bonusType) {
		
		uiFadeIn(spotLight,2f);
		yield return new WaitForSeconds(1.5f);
		
		//보너스 버튼 초기화
		initBonusButton(bonusType);
		
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

	
	
	//시체에 옷을 입혀준다
	private void showDeadBody() {

		string topItem = Utils.getTop().ToString();
		top.transform.Find( topItem ).gameObject.SetActive(true);
		top.transform.Find( topItem+"_bg" ).gameObject.SetActive(true);
		
		string bottomItem = Utils.getBottom().ToString();
		bottom.transform.Find( bottomItem ).gameObject.SetActive(true);
		bottom.transform.Find( bottomItem+"_bg" ).gameObject.SetActive(true);
		
		string shoesItem = Utils.getShoes().ToString();
		shoes.transform.Find( shoesItem ).gameObject.SetActive(true);
		shoes.transform.Find( shoesItem+"_bg" ).gameObject.SetActive(true);
	}
	
	
	//부활
	public void onClick_Rivial() {
		soundManager.clickPlay();
		Utils.responeGame();
		Destroy(bonusButtons);

	}

	
	//처음으로 이동
	public void onClick_Normal() {
		soundManager.clickPlay();
		Destroy(bonusButtons);
		StartCoroutine(goToMainScene(2f));
	}


	//보너스 포인트를 받고 메인페이지로 이동
	public void onClick_BonusPoint() {
		soundManager.clickPlay();
		Destroy(bonusButtons);

		int bonusPoint = (int)(Utils.getPlayData().point * 0.5f);
		Utils.modifyPoint( bonusPoint );
		StartCoroutine(goToMainScene(2f));
	}


	//결과페이지를 보여줌
	private void showPlayData() {
		PlayData playData = Utils.getPlayData();
		
		PlayDataResultText.text = 
			"Correct: " + playData.correct 
			+ "		Wrong:" + playData.wrong
			+ "		Point:" + playData.point;
	}
	
	
	
	IEnumerator goToMainScene(float delayTime) {
		fadeEffect.FadeOut(1f, 1f);
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadSceneAsync("MainScene");		
	}
}
