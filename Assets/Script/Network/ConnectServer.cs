using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectServer : MonoBehaviour {

	public AudioSource source;
	private MusicInfo musicInfo;
	private Quiz quiz;
	

	public QuizManager1 quizManager1;
	public QuizManager2 quizManager2;
	public NicknameManager nicknameManager;
	public MessageManager messageManager;
	public RankingManager rankingManager;	
	public GameObject networkDialog;
	public User user;
	
	/**
	 * 서버경로
	 */
	#if UNITY_EDITOR
		public static string SERVER_PATH = "http://localhost:9000/dduroon";
	#else
		public static string SERVER_PATH = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com:9000/dduroon";
	#endif

	public static string MUSIC_SERVER_PATH = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com/music";
	
	
	//초성맞히기
	public void quiz_1(int difficulty) {
		WWWForm form = new WWWForm();
		form.AddField("difficulty", difficulty);

		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/quiz_1", true, (www) => {
				Debug.Log("POST RESONSE callback OK!");
				musicInfo = JsonUtility.FromJson<MusicInfo>(www.downloadHandler.text);
				quizManager1.setGame( musicInfo );
			}
		));
	}
	
	
	//3지선다퀴즈
	public void quiz_2(int difficulty) {
		WWWForm form = new WWWForm();
		form.AddField("difficulty", difficulty);
		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/quiz_2", true, (www) => {
				quiz = JsonUtility.FromJson<Quiz>(www.downloadHandler.text);
				Debug.Log("POST RESONSE callback OK!");
				Debug.Log("quiz_pk:" + quiz.quiz_pk);

				musicInfo = quiz.musicInfo; //곡 정보 저장
				quizManager2.setGame( quiz );
			}
		));
	}
	
	
	//유저정보 업로드
    public void uploadUserInfo(UserInfo userInfo) {
     		WWWForm form = new WWWForm();
     		form.AddField("playgame_id", userInfo.playgame_id);
     		form.AddField("nickname", userInfo.nickname);
     		form.AddField("point", userInfo.point);
     		form.AddField("item", userInfo.item);
     		form.AddField("wear_top", userInfo.wear_top);
     		form.AddField("wear_bottom", userInfo.wear_bottom);
     		form.AddField("wear_shoes", userInfo.wear_shoes);
     		form.AddField("correct", userInfo.correct);
     		form.AddField("wrong", userInfo.wrong);
     		form.AddField("game_clear", userInfo.game_clear);
     		
     		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
     		StartCoroutine(postToServer(form, "/user/upload", false, (www) => {
			     ResponseUserUpload res = JsonUtility.FromJson<ResponseUserUpload>(www.downloadHandler.text);

			     if (res.user_pk != null) {
				     Debug.Log("서버로부터 pk정보를 다운받아 저장");
				     Utils.setUserPk( int.Parse(res.user_pk) );				     
			     }
			     
     		}));
     	}
	
	
	//유저정보 다운로드
	public void downloadUserInfo(string playgame_id) {
		WWWForm form = new WWWForm();
		form.AddField("playgame_id", playgame_id);
		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/user/download", true, (www) => {
				UserInfo userInfo = JsonUtility.FromJson<UserInfo>(www.downloadHandler.text);
				
				//서버에 기존 유저정보가 존재하면 그걸로 게임의 유저데이터를 업데이트
				if (!userInfo.nickname.Equals("")) {
					Utils.updateUserInfo( userInfo );
				}else {
					Debug.Log("서버에 이 유저의 정보가 없습니다");
				}
				
				//서버동기화 했다는 것을 기록
				Utils.setSyncServer(1);
				
				//동기화하거나 초기지급받은 옷을 입힌다
				user.wearCloth();
			}
		));
	}


	//등록가능한(중복되지 않은) 닉네임인지 확인
	public void nicknameCheck(string nickname) {
		WWWForm form = new WWWForm();
		form.AddField("nickname", nickname);
		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/user/nickname_check", true, (www) => {
				Response res = JsonUtility.FromJson<Response>(www.downloadHandler.text);
				
				if (res.result.Equals("ok")) {
					nicknameManager.nicknameOK();
				}else {
					nicknameManager.nicknameReject();
				}
				
			}
		));
	}
	
	
	//퀴즈1 피드백
	public void feedbackQuiz1(int music_pk, int correct) {
		WWWForm form = new WWWForm();
		form.AddField("music_pk", music_pk);
		form.AddField("correct", correct);

		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/feedback/quiz1", false, (www) => {
			Response res = JsonUtility.FromJson<Response>(www.downloadHandler.text);
			Debug.Log("성공스");
		}));
	}
	
	
	//퀴즈2 피드백
	public void feedbackQuiz2(int quiz_pk, int correct) {
		WWWForm form = new WWWForm();
		form.AddField("quiz_pk", quiz_pk);
		form.AddField("correct", correct);

		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/feedback/quiz2", false, (www) => {
			Response res = JsonUtility.FromJson<Response>(www.downloadHandler.text);
		}));
	}
	
	
	//메시지
	public void requestMessage(int count) {
		WWWForm form = new WWWForm();
		form.AddField("m_count", 3); //몇개

		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/message", false, (www) => {
			Messages ML = JsonUtility.FromJson< Messages >(www.downloadHandler.text);
			messageManager.setMessage( ML.messageList );
		}));
	}
	
	
	//유저 플레이데이터를 랭킹에 업로드
	public void uploadRanking(PlayData playData) {
		WWWForm form = new WWWForm();
		form.AddField("user_pk", Utils.getUserPk());
		form.AddField("correct", playData.correct);
		form.AddField("wrong", playData.wrong);
		form.AddField("point", playData.point);
		form.AddField("clear", playData.clear);


		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/ranking/upload", true,(www) => {
			Response res = JsonUtility.FromJson< Response >(www.downloadHandler.text);
		}));
	}
	
	//내 등수 얻기
	public void requestUserRank(int score) {
		WWWForm form = new WWWForm();
		form.AddField("score", score);
		
		Debug.Log("몇점?: " + score);

		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/ranking/user_rank", true, (www) => {
			MyRanking res = JsonUtility.FromJson< MyRanking >(www.downloadHandler.text);
			
			rankingManager.setUserRankingText( res.ranking );
		}));
	}
	
	
	
	
	//누적랭킹 리스트 얻기
	public void requestAccumulateRanking(int count) {
		WWWForm form = new WWWForm();
		form.AddField("count", count );
		

		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/ranking/accumulate", true, (www) => {
			AccumulateRankings res = JsonUtility.FromJson< AccumulateRankings >(www.downloadHandler.text);
			rankingManager.setRankBox( res.ranking );
		}));
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	//서버로 데이터를 post하는 코루틴
	IEnumerator postToServer(WWWForm form, string path, Boolean showDialogOnError, Action<UnityWebRequest> callback) {

		UnityWebRequest www = UnityWebRequest.Post(SERVER_PATH + path, form);
		yield return www.SendWebRequest();
		
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log("[네트워크에러]:" + www.error);
			if(showDialogOnError==true) networkDialog.active = true;
		}else {
			Debug.Log("[POST]");
			networkDialog.active = false;
			callback(www);
		}
	}


	public void stremingSound() {
		if (musicInfo!=null) StartCoroutine("streamingSound");
	}
	
	
	
	IEnumerator streamingSound() {
		string musicPath = MUSIC_SERVER_PATH + "/" + musicInfo.path;
		using (var www = new WWW(musicPath)){			
			yield return www; //다운받을동안 대기
			
			source.clip = www.GetAudioClip();
			source.Play();		
			
			//지정된 시간만큼 음악을 틀어준다
			float playTime = Utils.getPlayTime();
			StartCoroutine(stopMusic(playTime));
		}
	}
	
	IEnumerator stopMusic(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		source.Stop();
	}
}
