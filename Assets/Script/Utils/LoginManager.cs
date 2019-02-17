using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour {

	[SerializeField] private Text txtLog;
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
			txtLog.text = "Playgame SignIn Success!";

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
					txtLog.text = Social.localUser.id;
					
					Utils.setPlayGameId(Social.localUser.id);
					synchroUserInfo();
				}
				else
				{
					Debug.Log("로그인 실패");
					txtLog.text = "로그인 재시도 중입니다 (" + loginCount++ +")";
					StartCoroutine(reSignIn(2f)); //1초후 재시도
					
				}
			});
	}
	
	
    
    //재로그인
	IEnumerator reSignIn(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		txtLog.text = "유저 정보를 확인중입니다";
		
		#if UNITY_ANDROID
			synchroUserInfo();
		#else
			signIn();
		#endif
    }
	
	//서버와 유저데이터를 동기화
	public void synchroUserInfo() {
		
		Debug.Log("서버동기화: " + Utils.getSyncServer());
		
		//동기화가 되지 않은 상태이면 다운로드
		if ( Utils.getSyncServer()==0 ) {
			Debug.Log("유저정보 다운로드 시도");
			connectServer.downloadUserInfo( Social.localUser.id );
		}
		else {
			goToMainScene();
		}
	}

	//connectServer에서 호출
	public void goToMainScene() {
		Debug.Log("확인끝");
		SceneManager.LoadScene("MainScene");
	}
}
