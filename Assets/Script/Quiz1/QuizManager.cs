﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour{

   
	public Image[] answerBoxImg; //제출된 정답
	public Text[] initialTexts; //정답입력칸
	private int initialTextSize; //보기로 주어진 자음의 개수

	private bool isSountPlay = false;

	public ConnectServer connectServer;

	void Start() {
		connectServer.quiz_1(0,0);
	}

	
	//ConnectServer에서 이곳을 호출하면서 퀴즈 시작
	public void setGame( MusicInfo musicInfo) {

		GlobalScript.answerStr = "정답\n" + musicInfo.singer + " - " + musicInfo.title; //정답표시 설정		
		string answerInitial = musicInfo.initial;
		
		//보기자음은 답안길이 +3개
		initialTextSize = answerInitial.Length + 3;
	    
		//자음텍스트 객체들을 배열에 저장
		initialTexts = new Text[initialTextSize];
		for (int i = 0; i < initialTextSize; i++){
			initialTexts[i] = GameObject.Find("initText_Group").transform.Find("initText"+i).GetComponent<Text>();
			initialTexts[i].gameObject.SetActive(true);
			initialTexts[i].text = FakeText(); //임의의 자음 할당
		}
	    
		//임의의 자음텍스트중 답안 개수만큼 중복되지 않게 추출
		int pickCount = 0;
		List<int> picked = new List<int>();
	    
		while (pickCount < answerInitial.Length){
			int pick = Random.Range(0, initialTextSize - 1);

			bool overLap = false;
			for (int i = 0; i < picked.Count; i++){
				if (pick == picked[i]) overLap = true;
			}

			if (!overLap){
				pickCount++;
				picked.Add(pick);
			}
		}

		//추출한 위치에 정답 자음 삽입
		for (int i = 0; i < picked.Count; i++){
			initialTexts[picked[i]].text = answerInitial[i].ToString();
		}
	    
		//답안박스 객체들을 배열에 저장
		answerBoxImg = new Image[answerInitial.Length];
		for (int i = 0; i < answerInitial.Length; i++){
			answerBoxImg[i] = GameObject.Find("answerBox_Group").transform.Find("answerBox"+i).GetComponent<Image>();
			answerBoxImg[i].gameObject.SetActive(true);
		}

		//initialText에 답과 오답 섞어서 뿌려주기
		StartCoroutine(AnswerCheck(0, answerInitial));
	}
	
	
	//답을 체크하는 코루틴
	IEnumerator AnswerCheck(float delayTime, string answerInitial) { 
		yield return new WaitForSeconds(delayTime);
		
		//칸을 다 채웠는지 확인
		bool completeSubmit = true;
		for (int i = 0; i < answerInitial.Length; i++){
			if (answerBoxImg[i].GetComponent<AnswerBox>().mText == null){
				completeSubmit = false; 
			}
		}

		//칸을 다 채운경우, 정답이 맞는지 확인
		if (completeSubmit){
			
			bool result = true;
			for (int i = 0; i < answerInitial.Length; i++){
				if ( answerBoxImg[i].GetComponent<AnswerBox>().mText.text != answerInitial[i].ToString() ){
					Debug.Log(answerBoxImg[i].GetComponent<AnswerBox>().mText.text + " / " + answerInitial[i]);
					result = false;
				}
			}
			
			//정답일 때
			if (result) GlobalScript.lifeEvent = 0; //정답일때: 목숨 변동사항 없음
			else GlobalScript.lifeEvent = -1; //오답일때: 목숨 변동사항 있음
			
			//복귀
			SceneManager.LoadScene( GlobalScript.sceneName );
		}
		StartCoroutine(AnswerCheck(0.1f, answerInitial));
	}

	
	//임의의 자음텍스트 하나를 리턴
	string FakeText(){
		string consonant = "ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎㄲㅆㅃㅉ"; //18개
		int lotto = Random.Range(0, consonant.Length-1);
		return consonant[lotto].ToString();
	}
	
	
	//스마트폰을 터치하였을 때
	public void onClick_smartPhone() {
		if(isSountPlay) stopMusic();
		
		GameObject.Find("ConnectServer").GetComponent<ConnectServer>().stremingSound();
		GameObject.Find("Phone").transform.Find("playBtn").GetComponent<Image>().gameObject.SetActive(false);
		GameObject.Find("Phone").transform.Find("pauseBtn").GetComponent<Image>().gameObject.SetActive(true);
		isSountPlay = true;

		float playTime = GlobalScript.getPlayTime();
		StartCoroutine(stopMusic(playTime));
	}
	
	IEnumerator stopMusic(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		stopMusic();
	}

	//정지 처리
	void stopMusic() {
		GameObject.Find("Phone").transform.Find("playBtn").GetComponent<Image>().gameObject.SetActive(true);
		GameObject.Find("Phone").transform.Find("pauseBtn").GetComponent<Image>().gameObject.SetActive(false);
		isSountPlay = false;
	}
	
}
