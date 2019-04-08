using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager_Rewarded : MonoBehaviour
{
	
	private string androidGameId = "2887674";
	private string iosGameId = "2887673";
	private bool testMode;

	public GameObject bonusCanvas;
	private BonusType bonusType;
	public DieManager dieManager;
	
	private string gameId = null;
	
	private void Awake() {
	
		Debug.Log("ad init");
		
			#if UNITY_ANDROID
				gameId = androidGameId;
			#elif UNITY_IOS
				gameId = iosGameId;
			#endif
     
		
	
		//한번 부활한 경우 보너스 없음
		if (Utils.getPlayData().isRivival) {
			dieManager.initScene(BonusType.Normal);
			return;
		}
		
		//인생은 로또
		int lotto = Random.Range(0, 10);
		if (8 <= lotto && lotto <= 10) bonusType = BonusType.Rivival; //부활
		else if (6 <= lotto && lotto <= 7) bonusType = BonusType.PointBonus; //포인트 보너스
		else if( 0<= lotto && lotto <= 5) bonusType = BonusType.Normal; //일반
		
		
		//아이템을 구매했거나 보너스가 없으면 광고안봄
		if (Utils.getPlayData().ad == 0 || bonusType == BonusType.Normal) dieManager.initScene(bonusType);
		else startAd();
	}


	private void startAd() {
		
		Debug.Log("startAd()");
		
		//보너스 씬 표시
		bonusCanvas.active = true;

		//초기화
		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize(gameId, testMode);
		}
		StartCoroutine(playAd());
	}
	
	
	//광고재생
	IEnumerator playAd() {
		
		Debug.Log("playAd()");
		
		yield return new WaitForSeconds(1f);
		if (Advertisement.IsReady("rewardedVideo"))
		{
			// 광고가 끝난 뒤 콜백함수 "HandleShowResult" 호출
			Utils.modifyPlaydataAd(-1); //시청해야 되는 광고횟수 -1
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
		else {
			StartCoroutine(playAd());
		}
	}



	
    // 광고가 종료된 후 호출
	private void HandleShowResult(ShowResult result)
	{
		bonusCanvas.active = false;
		dieManager.initScene(bonusType);

		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
		
		
	}
}
