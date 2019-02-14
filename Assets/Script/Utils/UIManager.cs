using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

//	public Image image;
	public Text midText;

	public Text nicknameText;

	public Image lifeBar; //생명바
	public Text scoreText; //점수표시
	public Image userDieBackgroud; //게임오버

	public GameObject answerImg; //답변페이지
	public Text answerTitle; //노래제목
	public Text answerSinger; //가수

	public GameObject userScore;

	private const int LIFE_UNIT = 116; //목숨 한칸의 양
	private const int LIFE_MAX = 350; //최대 목숨의 양
	private int lifebarCount; //코루틴에서 라이프바 수정할때 쓰는 타이머

	public void showText(string str) {
		midText.text = str;
		FadeIn(midText, 0.5f);
		StartCoroutine("stayText", 1.5f);
	}


	public void showText_Long(string str) {
		midText.text = str;
		FadeIn(midText, 0.5f);
		StartCoroutine("stayText", 4f);
	}


	IEnumerator stayText(float delayTime) {
		yield return new WaitForSeconds(delayTime); //표시시간
		FadeOut(midText, 0.5f);
	}

	//닉네임 표시
	public void setNickname() {
		nicknameText.text = Utils.getNickname();
	}

	//정답을 표시
	public void showAnswer() {
		answerImg.SetActive(true);
		answerTitle.text = Utils.getAnswerTitle();
		answerSinger.text = Utils.getAnswerSinger();
		
		//정답창 5초후에 끈다
		StartCoroutine(hideAnswer(5f));
	}

	
	IEnumerator hideAnswer(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		answerImg.GetComponent<Animator>().SetTrigger("answerHide_t");
	}
	
	
	public static void FadeIn(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(0f);
		g.CrossFadeAlpha(1f, t, false);//second param is the time
	}
	
	
	public static void FadeOut(Graphic g, float t){
		g.GetComponent<CanvasRenderer>().SetAlpha(1f);
		g.CrossFadeAlpha(0f, t, false);
	}
	
	
	//라이프바 한칸 내림
	//라이프바의 크기는 500이며 한칸의 크기는 166이다
	public void decreaseLifeBar() {
		lifebarCount = 0;
		StartCoroutine(modifyLifeBarSize(0, -1));
	}

	
	//라이프바 한칸 올림
	public void increaseLifeBar() {
		lifebarCount = 0;
		StartCoroutine(modifyLifeBarSize(0, 1));
	}
	
	
	IEnumerator modifyLifeBarSize(float delayTime, int val) { //val: +1, -1
		yield return new WaitForSeconds(delayTime);
		
		RectTransform rt = lifeBar.rectTransform;
		float nowWidth = rt.rect.width;
		float nowHeight = rt.rect.height;
		
		rt.sizeDelta = new Vector2 (nowWidth+(1f*val), nowHeight);

		lifebarCount++;
		if (lifebarCount < LIFE_UNIT) StartCoroutine(modifyLifeBarSize(0.005f, val));
	}

	
	//라이프바 값에 맞게 업데이트
	public void updateLifeBar() {
		RectTransform rt = lifeBar.rectTransform;
		float nowWidth = Utils.getLife() * LIFE_UNIT;
		float nowHeight = rt.rect.height;
		rt.sizeDelta = new Vector2 (nowWidth, nowHeight);
	}


	//게임오벼 연출
	public void UIUserDie() {
		userDieBackgroud.gameObject.SetActive(true);
		FadeIn(userDieBackgroud, 4f);
		
		StartCoroutine(DieEvent(4));
	}
	

	IEnumerator DieEvent(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene("DieScene");
	}
	
	
	//현재 점수 표시
	public void setPointText() {
		scoreText.text = Utils.getPoint().ToString();
		
		//글씨크기에 맞게 박스크기 조정
		scoreText.rectTransform.sizeDelta = new Vector2(scoreText.preferredWidth, scoreText.preferredHeight); 
	}
	
	
	//캐릭터 위에 점수상승 이펙트
	public void raisePoint(int point) {
		userScore.GetComponent<Text>().text = "+" + point;
		userScore.GetComponent<Animator>().SetTrigger("score_t");
	}

}
