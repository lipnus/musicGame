using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager_Rewarded : MonoBehaviour
{
	
	private string androidGameId = "2887674";
	private string iosGameId = "2887673";
	private bool testMode;

	private BonusType bonusType;

	public DieManager dieManager;
	
	private void Awake() {
	
		Debug.Log("ad init");
		string gameId = null;
     
			#if UNITY_ANDROID
			gameId = androidGameId;
			#elif UNITY_IOS
            gameId = iosGameId;
			#endif
     
		
		//초기화
		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize(gameId, testMode);
		}
 
		
		//한번 부활한 경우 보너스 없음
		if (Utils.getPlayData().isRivival) {
			dieManager.initScene(BonusType.Normal);
			return;
		}
		
		int lotto = Random.Range(0, 10);
		if (8 < lotto) bonusType = BonusType.Rivival; //부활
		else if (2 < lotto) bonusType = BonusType.PointBonus; //포인트 보너스
		else bonusType = BonusType.Normal; //일반
		
		
		//봐야할 광고수만큼 광고시청(아이템 구매했으면 getPlayData().ad가 0임)
		if (Utils.getPlayData().ad > 0) ShowRewardedAd(); 
		else dieManager.initScene(bonusType);
	}
	
	
	//광고 보여주기
	public void ShowRewardedAd()
	{
		Debug.Log("ShowRewardedAd()");
		if (Advertisement.IsReady("rewardedVideo"))
		{
            // 광고가 끝난 뒤 콜백함수 "HandleShowResult" 호출
			Utils.modifyPlaydataAd(-1); //시청해야 되는 광고횟수 -1
            var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	
    // 광고가 종료된 후 호출
	private void HandleShowResult(ShowResult result)
	{
		
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
