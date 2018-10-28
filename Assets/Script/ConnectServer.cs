using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConnectServer : MonoBehaviour {

	string url;
	public AudioSource source;
	
	
	void Start() {
		Debug.Log("Start()");
		StartCoroutine("postData", 0);

		
		
		//사운드
		url = "http://ec2-13-125-247-189.ap-northeast-2.compute.amazonaws.com/music/likeit.mp3";
		StartCoroutine("streamingSound", "글자도 전달이 됩니까?");
	}

	void postToServer() {
		
	}
	
	IEnumerator postData() {
		WWWForm form = new WWWForm();
		form.AddField("genre", "0");
		form.AddField("order", "0");
		
		UnityWebRequest www = UnityWebRequest.Post(GlobalScript.serverPath + "/quiz_1", form);
		yield return www.SendWebRequest();
		
		Debug.Log("제이슨: " + www.downloadHandler.text);
		MusicInfo musicInfo = JsonUtility.FromJson<MusicInfo>(www.downloadHandler.text);
		Debug.Log("결과: " + musicInfo.title);


		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}else {
			Debug.Log("Form upload complete!");
		}
	}
	
	
	
	
	IEnumerator streamingSound(string a){
		Debug.Log(a);
		using (var www = new WWW(url)){			
			yield return www; //다운받을동안 대기

			Debug.Log("준비완료");
			source.clip = www.GetAudioClip();
			source.Play();		
		}
	} 
	
}
