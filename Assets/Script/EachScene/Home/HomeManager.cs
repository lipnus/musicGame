using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour {

	public float userSpeed;
	public GameObject user;
	public GameObject stand_light_on;
	public GameObject stand_light_off;

	public GameObject dark1;
	public GameObject dark2;

	public AudioSource switchSound;
	public AudioSource tickingSound;
	public AudioSource blanketSound;
	public AudioSource okSound;

	public Button skipButton;
	public UIManager uiManager;


	// Use this for initialization
	void Start () {
		tickingSound.Play();
		StartCoroutine("Scenario");
	}

	IEnumerator Scenario() { 
		yield return new WaitForSeconds(1);
		uiManager.showText("잠이 오지 않아..");
		yield return new WaitForSeconds(2);

		//불켜기
		tickingSound.Stop();
		stand_light_on.SetActive(true);
		stand_light_off.SetActive(false);
		dark1.SetActive(false);
		dark2.SetActive(true);
		switchSound.Play();
		uiManager.showText("공허한 마음을 달래줄 것은 오직 음악..");
		yield return new WaitForSeconds(1);
		
		//일어남
		blanketSound.Play();
		GameObject.Find("Bed").GetComponent<Animator>().SetTrigger("out_t");
		user.SetActive(true);
		
		//걷기시작
		user.GetComponent<Animator>().SetTrigger("walking_t");
		StartCoroutine("walking", 0);
		
		yield return new WaitForSeconds(5);
		startStage();
		
		
	}

	
	//게임을 시작하지
	public void startStage() {
		
		//게임플레이데이터(게임한판에 대한 데이터) 초기화
		Utils.resetPlayData();
		
		
		//튜토리얼의 완료유무에 따른 스테이지 이동
		if (Utils.isGuide_Finished() == true) {
			Debug.Log("튜토리얼 이미 봤음");
			Utils.changeScene("Stage 1", "Seoul City", "잠실인근", "CityScene");
			
		}
		else {
			Debug.Log("튜토리얼 봐야함");
			Utils.changeScene("Tutorial stage", "Seoul City", "서울도심 어딘가", "TutorialScene" );
		}
	}


	//스킵버튼을 눌렀을 때
	public void onClick_Skip() {
		skipButton.enabled = false;
		okSound.Play();
		startStage();
	}
	
	
	//걷기루틴
	IEnumerator walking(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
		user.transform.Translate(Vector3.right * userSpeed);
		StartCoroutine("walking", 0.01f);
	}
	
}
