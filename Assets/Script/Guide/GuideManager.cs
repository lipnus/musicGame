using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour {

	public GameObject fieldManager;
	public GameObject guideObj; //가이드가 발동되면 여기가 등록시켜두고, 클릭시 해당 가이드 파괴
	public GameObject midText;
	public GameObject user;
	
	private bool touchOK; //그냥 슉 지나가는거 방지하기 위해 좀 딜레이를 준 다음 보여준다.
	private const float TOUCH_DELAY = 0.5f;//터치 딜레이


	public void enrollGuideObj(GameObject guideObj) {
		this.guideObj = guideObj;
		touchOK = false;
		StartCoroutine(authorizeTouch(TOUCH_DELAY));

	}

	IEnumerator authorizeTouch(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		touchOK = true;
	}
	
	
	//가이드와 점프 모두 화면터치를 입력받으므로 그 두가지를 구분해주는 역할
	public void onClick_JumpAndGuide() {
		
		//가이드 이벤트가 발동되지 않았을때
		if (guideObj == null) {
			user.GetComponent<User>().Jump();
			return;
		}
		
		//가이드이벤트발동 & 발동후 일정시간이 지남
		if (touchOK) {
			
			if (guideObj.transform.name.Equals("Guide-jump")) user.GetComponent<User>().Jump(); //점프퀘스트
			else if(guideObj.transform.name.Equals("Guide-cat1")) fieldManager.GetComponent<StageFieldManager>().quizStart("Quiz_initial");
			else if(guideObj.transform.name.Equals("Guide-cat2")) fieldManager.GetComponent<StageFieldManager>().quizStart("Quiz_choice");
			else if(guideObj.transform.name.Equals("Guide-subway")) GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(3f); //페이드아웃
			else if (guideObj.transform.name.Equals("Guide-end")) {
				Utils.changeScene("Stage 1", "Seoul City", "잠실인근", "CityScene");
			}
			
			midText.GetComponent<Animator>().SetBool("showText", false);
			fieldManager.GetComponent<StageFieldManager>().resumeMove();
			Destroy(guideObj);		
		}
	}


	//가이드 종료(퀴즈에서 더이상 가이드가 안나온다)
	public void endGuide() {
		Utils.endGuide();
	}
	 
}
