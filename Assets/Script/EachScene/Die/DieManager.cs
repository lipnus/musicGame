using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieManager : MonoBehaviour {

	public Image spotLight;
	public Text text;
	public Image black;
	public UnityAdsManager_Rewarded ad_reward;

	public GameObject rivivalButton;
	public GameObject pointButton;
	public GameObject normalButton;

	public GameObject top;
	public GameObject bottom;
	public GameObject shoes;

	public Text PlayDataResultText;
	
	// Use this for initialization
	void Start () {

		if (Utils.playData.ad > 0) ad_reward.ShowRewardedAd(); //광고 봐야될거 있으면 광고튼다
		else initScene();
	}

	
	//광고종료시에 얘를 호출
	public void initScene() {
		
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
		
		//보너스 버튼 초기화
		initBonusButton();
		
		//코루틴시작
		StartCoroutine(dieScenario(0f));
	}
	
	
	
	//랜덤으로 보너스 버튼을 띄운다
	private void initBonusButton() {

		//한번 부활한 경우 보너스는 없다
		if (Utils.playData.isRivival) {
			normalButton.active = true;
			return;
		}
		
		int lotto = Random.Range(0, 10);
		if (8 < lotto) {
			rivivalButton.active = true;
			Utils.playData.isRivival = true;
		}else if (4 < lotto) {
			pointButton.active = true;		
		}else {
			normalButton.active = true;
		}
	}

	//스포트라이트 먼저
	IEnumerator dieScenario(float delayTime) {
		
		uiFadeIn(spotLight,2f);
		yield return new WaitForSeconds(1.5f);
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
		top.transform.FindChild( topItem ).gameObject.SetActive(true);
		top.transform.FindChild( topItem+"_bg" ).gameObject.SetActive(true);
		
		string bottomItem = Utils.getBottom().ToString();
		bottom.transform.FindChild( bottomItem ).gameObject.SetActive(true);
		bottom.transform.FindChild( bottomItem+"_bg" ).gameObject.SetActive(true);
		
		string shoesItem = Utils.getShoes().ToString();
		shoes.transform.FindChild( shoesItem ).gameObject.SetActive(true);
		shoes.transform.FindChild( shoesItem+"_bg" ).gameObject.SetActive(true);
	}
	
	
	//광고보고 부활(광고에서 콜백으로 여기 호출)
	public void onClick_Rivial() {
		Utils.responeGame();
		SceneManager.LoadScene( Utils.sceneName );
	}

	
	//처음으로 이동
	public void onClick_Normal() {
		SceneManager.LoadSceneAsync("MainScene");
	}


	//보너스 포인트를 받고 메인페이지로 이동
	public void onClick_BonusPoint() {
		int bonusPoint = (int)(Utils.getPlayData().point * 0.5f);
		Utils.modifyPoint( bonusPoint );
		
		SceneManager.LoadSceneAsync("MainScene");
	}


	//결과페이지를 보여줌
	private void showPlayData() {
		PlayData playData = Utils.getPlayData();
		
		PlayDataResultText.text = 
			"Correct: " + playData.correct 
			+ "		Wrong:" + playData.wrong
			+ "		Point:" + playData.point;
	}
}
