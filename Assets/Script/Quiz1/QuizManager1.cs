using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager1 : MonoBehaviour{

   
	public Image[] answerBoxImg; //제출된 정답
	public Text[] initialTexts; //정답입력칸
	private int initialTextSize; //보기로 주어진 자음의 개수
	public GameObject midText;
	public GameObject midBack;
	public GameObject loadingText;
	public SoundManager soundManager;

	private bool isSountPlay = false;
	private MusicInfo musicInfo;

	public ConnectServer connectServer;

	void Start() {
		connectServer.quiz_1(0);
		
		//n동안 가이드 텍스트 표시
		if(!Utils.isGuide_Finished()) StartCoroutine(showGuideText(0.8f));

	}

	IEnumerator showGuideText(float delayTime) {
		midText.active = true;
		midBack.active = true;
		midText.GetComponent<Animator>().SetBool("showText", true);

		yield return new WaitForSeconds(delayTime);
	
		midText.GetComponent<Animator>().SetBool("showText", false);
		yield return new WaitForSeconds(0.3f);
		midBack.active = false;
	}

	
	//ConnectServer에서 이곳을 호출하면서 퀴즈 시작
	public void setGame( MusicInfo musicInfo ) {
		
		//음원정보를 전역변수에 지정		
		this.musicInfo = musicInfo;
		
		//필드에서 표시해줄 답안등록
		Utils.setAnswer(musicInfo.title, musicInfo.singer);
		
		string answerInitial = musicInfo.initial;
		
		//보기자음은 답안길이 +a개
		initialTextSize = answerInitial.Length + 1;
	    
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
		if (completeSubmit) {
			
			loadingText.active = true; //로딩
			soundManager.okPlay();
			
			bool result = true;
			for (int i = 0; i < answerInitial.Length; i++){
				if ( answerBoxImg[i].GetComponent<AnswerBox>().mText.text != answerInitial[i].ToString() ){
					Debug.Log(answerBoxImg[i].GetComponent<AnswerBox>().mText.text + " / " + answerInitial[i]);
					result = false;
				}
			}
			
			//정답일 때
			if (result){
				Utils.lifeEvent = 0; //정답일때: 목숨 변동사항 없음
				connectServer.feedbackQuiz1(int.Parse(musicInfo.music_pk),1);
			}else{
				Utils.lifeEvent = -1; //오답일때: 목숨 변동사항 있음
				connectServer.feedbackQuiz1(int.Parse(musicInfo.music_pk),0);
			}
			
			//복귀
			SceneManager.LoadSceneAsync( Utils.sceneName );
		}
		else StartCoroutine(AnswerCheck(0.1f, answerInitial));
	}

	
	//임의의 자음텍스트 하나를 리턴
	string FakeText(){
		string consonant = "ㄱㄴㄷㄹㅁㅂㅅㅇㅈㅊㅋㅌㅍㅎㄲㅆㅃㅉ"; //18개
		int lotto = Random.Range(0, consonant.Length-1);
		return consonant[lotto].ToString();
	}
	
	
	//스마트폰을 터치
	public void onClick_smartPhone() {
		soundManager.clickPlay();
		if(isSountPlay) stopMusic();
		
		GameObject.Find("connectServer").GetComponent<ConnectServer>().stremingSound();
		GameObject.Find("Phone").transform.Find("playBtn").GetComponent<Image>().gameObject.SetActive(false);
		GameObject.Find("Phone").transform.Find("pauseBtn").GetComponent<Image>().gameObject.SetActive(true);
		isSountPlay = true;
		
		float playTime = Utils.getPlayTime();
		StartCoroutine(stopMusic(playTime));
	}
	
	IEnumerator stopMusic(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		stopMusic();
	}

	//음악정지
	void stopMusic() {
		GameObject.Find("Phone").transform.Find("playBtn").GetComponent<Image>().gameObject.SetActive(true);
		GameObject.Find("Phone").transform.Find("pauseBtn").GetComponent<Image>().gameObject.SetActive(false);
		isSountPlay = false;
	}
	
	
	//포기
	public void onClick_giveUp() {
		soundManager.okPlay();
		loadingText.active = true; //로딩
		Utils.lifeEvent = -1; //오답일때: 목숨 변동사항 있음
		connectServer.feedbackQuiz1(int.Parse(musicInfo.music_pk),0); //피드백처리
		SceneManager.LoadSceneAsync( Utils.sceneName );
	}

	//고양이 손 터치
	public void onClick_CatHand() {
		soundManager.catPlay();
	}
	
}
