using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectServer : MonoBehaviour {

	public AudioSource source;
	private MusicInfo musicInfo;
	private Quiz quiz;
	//테스트용경로: "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com/music/likeit.mp3";

	
	//초성맞히기
	public void quiz_1(int genre, int order) {
		WWWForm form = new WWWForm();
		form.AddField("genre", genre);
		form.AddField("order", order);
		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/quiz_1", (www) => {
				musicInfo = JsonUtility.FromJson<MusicInfo>(www.downloadHandler.text);
				Debug.Log("POST RESONSE callback OK!");

				GameObject.Find("QuizManager").GetComponent<QuizManager1>().setGame( musicInfo );
			}
		));
	}
	
	//3지선다퀴즈
	public void quiz_2(int difficulty) {
		WWWForm form = new WWWForm();
		form.AddField("difficulty", difficulty);
		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/quiz_2", (www) => {
				quiz = JsonUtility.FromJson<Quiz>(www.downloadHandler.text);
				Debug.Log("POST RESONSE callback OK!");
				Debug.Log("quiz_pk:" + quiz.quiz_pk);

				musicInfo = quiz.musicInfo; //곡 정보 저장
				GameObject.Find("QuizManager").GetComponent<QuizManager2>().setGame( quiz );
			}
		));
	}
	
	
	//서버로 데이터를 post하는 코루틴
	IEnumerator postToServer(WWWForm form, string path, Action<UnityWebRequest> callback) {

		UnityWebRequest www = UnityWebRequest.Post(GlobalScript.serverPath + path, form);
		yield return www.SendWebRequest();
		
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log("[Error]:" + www.error);
		}else {
			Debug.Log("Form upload complete!");
		}
		callback(www);
	}


	public void stremingSound() {
		if (musicInfo!=null) StartCoroutine("streamingSound");
	}
	
	
	IEnumerator streamingSound() {
		string musicPath = GlobalScript.musicPath + "/" + musicInfo.path;
		using (var www = new WWW(musicPath)){			
			yield return www; //다운받을동안 대기
			
			Debug.Log("??");
			
			source.clip = www.GetAudioClip();
			source.Play();		
			
			//지정된 시간만큼 음악을 틀어준다
			float playTime = GlobalScript.getPlayTime();
			StartCoroutine(stopMusic(playTime));
		}
	}
	
	IEnumerator stopMusic(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		source.Stop();
	}
}
