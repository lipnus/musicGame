using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager2 : MonoBehaviour{


	private bool isSountPlay = false;
	public ConnectServer connectServer;
	public GameObject sight;
	public GameObject bar;
	public List<Image> button = new List<Image>();
	public List<Image> selectedImg = new List<Image>();
	public List<Text> choiceText = new List<Text>();


	public GameObject midText;
	public GameObject midBack;
	
	private Quiz quiz;

	void Start() {
		connectServer.quiz_2(0);
		sightMove();
				
		//초기화(올라가기 전엔 안보임)
		for (int i = 0; i < button.Count; i++) {
			button[i].GetComponent<CanvasRenderer>().SetAlpha(0f);
			choiceText[i].GetComponent<CanvasRenderer>().SetAlpha(0f);
			button[i].enabled = false;
			choiceText[i].enabled = false;
		}
		
		//첫 퀴즈인 경우 가이드 텍스트 표시
		if(!GlobalScript.isGuide_Finished()) StartCoroutine(showGuideText(3f));

	}

	IEnumerator showGuideText(float delayTime) {
		midText.active = true;
		midBack.active = true;
		midText.GetComponent<Animator>().SetBool("showText", true);

		yield return new WaitForSeconds(delayTime);
	
		midText.GetComponent<Animator>().SetBool("showText", false);
		yield return new WaitForSeconds(0.5f);
		midBack.active = false;
	}
	

	void sightMove() {
		sight.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 610);
		StartCoroutine(appearQuizUI(2f));
	}

	IEnumerator appearQuizUI(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		bar.SetActive(true);
		
		//다 올라가고 등장시작
		for (int i = 0; i < button.Count; i++) {
			button[i].enabled = true;
			choiceText[i].enabled = true;
		}
		
		for (int i = 0; i < button.Count; i++) {
			uiFadeIn(button[i], 1f);
			uiFadeIn(choiceText[i], 1.5f);
		}
	}
	
	//페이드인
	void uiFadeIn(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(1f, t, false);//second param is the time
	}
	
	//페이드아웃
	void uiFadeOut(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(1f);
		g.CrossFadeAlpha(0f, t, false);
	}
	
	//ConnectServer에서 이곳을 호출하면서 퀴즈 시작
	public void setGame(Quiz q) {
		quiz = q; //전역에 할당
		
		//필드에서 표시해줄 답안등록
		GlobalScript.setAnswer(quiz.musicInfo.title, quiz.musicInfo.singer);

		for (int i = 0; i < choiceText.Count; i++) {
			choiceText[i].text = quiz.choices[i].choice;
		}
	}

	public void onClick_choice(int choice) {
		Debug.Log("초이스: " + choice);
		
		//다른 버튼은 없어짐
		for (int i = 0; i < button.Count; i++) {
			if (i != choice) uiFadeOut(button[i], 1f);
			if (i != choice) uiFadeOut(choiceText[i], 1f);
		}
		
		//선택한 버튼 페이드인
		uiFadeIn(selectedImg[choice], 1f);

		//선택한 버튼 차오름
		selectedImg[choice].fillAmount = 0.2f; //초깃값
		choiceText[choice].color = new Color(0f, 0f, 0f);
		StartCoroutine(imgFilled(choice, 0));
	}

	IEnumerator imgFilled(int choice, float delayTime) {
		yield return new WaitForSeconds(delayTime);
		selectedImg[choice].fillAmount += 0.02f;
		if (selectedImg[choice].fillAmount < 1f) StartCoroutine(imgFilled(choice, 0.001f));
		else {
			yield return new WaitForSeconds(0.5f);
			endQuiz(choice);
		}
	}

	//답안제출
	void endQuiz(int choice) {
		
		//정답일 때
		if ( quiz.choices[choice].truth==1 ) GlobalScript.lifeEvent = 0; //정답일때: 목숨 변동사항 없음
		else GlobalScript.lifeEvent = -1; //오답일때: 목숨 변동사항 있음
			
		//복귀
		SceneManager.LoadScene( GlobalScript.sceneName );
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
