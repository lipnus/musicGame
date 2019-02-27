using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour {

	public ConnectServer connectServer;
	private int loginCount = 0; //로그인 재시도 횟수 표시

	private void Awake()
	{
		PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
	}


	void Start() {

		signIn();
	}
	
	void signIn() {
		
		//이미 인증된 사용자는 바로 로그인 성공된다.
		if (Social.localUser.authenticated)
		{
			//로그인 성공
			Debug.Log(Social.localUser.userName);
			Utils.setPlayGameId(Social.localUser.id);
			synchroUserInfo();
		}
		else
			Social.localUser.Authenticate((bool success) =>
			{
				if (success)
				{
					//로그인 성공
					Debug.Log(Social.localUser.userName);					
					Utils.setPlayGameId(Social.localUser.id);
					synchroUserInfo();
				}
				else
				{
					Debug.Log("로그인 실패");
					StartCoroutine(reSignIn(2f)); //1초후 재시도					
				}
			});
	}
	
	
    
    //재로그인
	IEnumerator reSignIn(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		
		#if UNITY_ANDROID
			synchroUserInfo();
		#else
			signIn();
		#endif
    }
	
	//서버와 유저데이터를 동기화
	public void synchroUserInfo() {
		
		Debug.Log("서버동기화: " + Utils.getSyncServer());
		
		//동기화가 되지 않은 상태(최초실행)
		if ( Utils.getSyncServer()==0 ) {
			Debug.Log("유저정보 다운로드 시도");
			
			//유저 초기화(서버에서 동기화가 되면 이것들은 덮어씌워짐)
			Utils.firstGift();
			Utils.setPoint(1);
			
			connectServer.downloadUserInfo( Social.localUser.id ); //connectServer 콜백에서 syncServer변수를 1로만듦
		}
		
		//동기화가 되어있으면 업로드
		else {
			Debug.Log("유저정보 업로드");
			connectServer.uploadUserInfo( Utils.getUserInfo() );
		}
	}

}
