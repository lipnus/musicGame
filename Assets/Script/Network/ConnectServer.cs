using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectServer : MonoBehaviour {

	public AudioSource source;
	private MusicInfo musicInfo;
	private Quiz quiz;

	public QuizManager1 quizManager1;
	public QuizManager2 quizManager2;

	public GameObject networkDialog;
	

	
	//초성맞히기
	public void quiz_1(int difficulty) {
		WWWForm form = new WWWForm();
		form.AddField("genre", difficulty);
		form.AddField("order", 0);

		
		//코루틴으로 서버에 접속하고 완료 시, 콜백으로 받아온다
		StartCoroutine(postToServer(form, "/quiz_1", (www) => {
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
		StartCoroutine(postToServer(form, "/quiz_2", (www) => {
				quiz = JsonUtility.FromJson<Quiz>(www.downloadHandler.text);
				Debug.Log("POST RESONSE callback OK!");
				Debug.Log("quiz_pk:" + quiz.quiz_pk);

				musicInfo = quiz.musicInfo; //곡 정보 저장
				quizManager2.setGame( quiz );
			}
		));
	}
	
	
	//서버로 데이터를 post하는 코루틴
	IEnumerator postToServer(WWWForm form, string path, Action<UnityWebRequest> callback) {

		UnityWebRequest www = UnityWebRequest.Post(Utils.serverPath + path, form);
		yield return www.SendWebRequest();
		
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log("[네트워크에러]:" + www.error);
			networkDialog.active = true;
		}else {
			Debug.Log("Form upload complete!");
			networkDialog.active = false;
			callback(www);
		}
	}


	public void stremingSound() {
		if (musicInfo!=null) StartCoroutine("streamingSound");
	}
	
	
	IEnumerator streamingSound() {
		string musicPath = Utils.musicPath + "/" + musicInfo.path;
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
