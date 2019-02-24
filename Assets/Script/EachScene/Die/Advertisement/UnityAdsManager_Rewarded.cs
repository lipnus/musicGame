using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager_Rewarded : MonoBehaviour
{
	
	private string androidGameId = "2887674";
	private string iosGameId = "2887673";
	private bool testMode;

	public DieManager dieManager;
	
	private void Awake() {
	
		Debug.Log("ad init");
		string gameId = null;
     
			#if UNITY_ANDROID
			gameId = androidGameId;
			#elif UNITY_IOS
            gameId = iosGameId;
			#endif
     
		
		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize(gameId, testMode);
		}
		
		
		if (Utils.getPlayData().ad > 0) ShowRewardedAd(); //광고 봐야될거 있으면 광고튼다
		else dieManager.initScene();
	}
	
	
	//광고 보여주기
	//DieScene의 onStart()에서 호출
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

    // 광고가 종료된 후 자동으로 호출되는 콜백 함수
	private void HandleShowResult(ShowResult result)
	{
		
		dieManager.initScene();

		
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
