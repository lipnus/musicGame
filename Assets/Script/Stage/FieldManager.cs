using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class FieldManager : MonoBehaviour {

	public List<GameObject> layer=new List<GameObject>();
	public List<float> layer_speed =new List<float>();
	
	public GameObject user;
	public GameObject near;

	private const float BEFORE_QUIZ_POSITION = 4;
	float userSpeed;

	void Update () {

		for (int i = 0; i < layer.Count; i++) {
			layer[i].transform.Translate(Vector3.right * (userSpeed-layer_speed[i]) * Time.deltaTime);
		}
	}
	
	
	void Start () {
		
		//테스트용, 반드시 지울것
		GlobalScript.setLife(3);
		
		//페이드인 효과
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeIn(1f);
		
		userSpeed = GameObject.Find("User").GetComponent<User>().userSpeed; //속도
		
		//점수표시
		GlobalScript.showScoreText();
		
		//목숨표시(Life만큼의 칸을 표시해줌)
		GameObject.Find("UIManager").GetComponent<UIManager>().updateLifeBar();
		
		//음악퀴즈를 풀고 돌아온 경우
		if (!GlobalScript.userPosition.Equals(new Vector3(0, 0, 0))) {			
			Debug.Log("Quiz에서 복귀");
			returnFromMusicQuiz();
		}
	}
	
	
	//음악퀴즈에서 필드로 돌아왔을때의 처리
	void returnFromMusicQuiz() {
		string answerStr = GlobalScript.answerStr;
		
		//레이어들의 위치를 퀴즈풀기 이전으로 복구
		loadPosition();
		
		//정답표시
		GameObject.Find("UIManager").GetComponent<UIManager>().showText_Long(answerStr);
		
		//목숨처리
		if (GlobalScript.lifeEvent == 0) {//정답
			StartCoroutine(correctIcon((3f)));
		}else if (GlobalScript.lifeEvent == -1) {//오답
			StartCoroutine(wrongIcon((3f)));
			GameObject.Find("UIManager").GetComponent<UIManager>().decreaseLifeBar();
			GlobalScript.modifyLife(-1); //이건 반드시 decreaseLifeBar뒤에 와야한다
			
			//사망여부확인
			if (GlobalScript.getLife() <= 0) {
				savePosition(); //부활을 대비해서 현제상태 기억
				StartCoroutine(userDie(1));
			}
		}
	}

	IEnumerator correctIcon(float delayTime) {
		GameObject.Find("correct_icon").GetComponent<Animator>().SetBool("correct_b", true);
		yield return new WaitForSeconds(delayTime);
		GameObject.Find("correct_icon").GetComponent<Animator>().SetBool("correct_b", false);
	}
	
	IEnumerator wrongIcon(float delayTime) {
		GameObject.Find("wrong_icon").GetComponent<Animator>().SetBool("wrong_b", true);
		yield return new WaitForSeconds(delayTime);
		GameObject.Find("wrong_icon").GetComponent<Animator>().SetBool("wrong_b", false);
	}

	
	//[게임오버] 메인 코루틴
	IEnumerator userDie(float delayTime) {

		//UI게임오버효과
		GameObject.Find("UIManager").GetComponent<UIManager>().UIUserDie(); 
		yield return new WaitForSeconds(delayTime);
		
		//기력딸려서 속도느려짐
		GameObject.Find("User").GetComponent<User>().userSpeed = 0.1f;
		
		//죽음을 직감한 멘트
		GameObject.Find("UIManager").GetComponent<UIManager>().showText_Long("텐션이 떨어진다...");
	}

 

	//고양이 만났을 때
	public void catEffect() {
//		//투시시점
//		Camera.main.orthographic = false;
//		GameObject.Find("Sky").transform.localScale += new Vector3(2f, 2f, 0);
//	
//		//카메라초점거리
//		GameObject.Find("Main Camera").GetComponent<Camera>().focalLength = 4f;
//		
//		StartCoroutine("rotateCamera", 0.1);
	}
	
	
	//카메라 시점변화
	IEnumerator rotateCamera(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
	
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		cam.transform.Rotate(Vector3.up * 0.1f); 
		cam.transform.Translate( Vector3.back * 0.005f);
	
		StartCoroutine("rotateCamera", 0.1f);
	}

	//모든 레이어의 위치를 전역에 저장
	public void savePosition() {
		
		GlobalScript.sceneName = Application.loadedLevelName; //스테이지 기억
		
		//레이어위치 저장
		GlobalScript.positionHolder.Clear();
		for (int i = 0; i < layer.Count; i++) {
			GlobalScript.positionHolder.Add( layer[i].transform.position );
		}
		
		//유저위치 저장
		GlobalScript.userPosition = user.transform.position;
	}
	
	//모든 레이어의 위치를 전역에서 불러옴
	public void loadPosition() {

		//레이어 복구
		for (int i = 0; i < layer.Count; i++) {
			Vector3 lPosition = GlobalScript.positionHolder[i];
			lPosition.x += BEFORE_QUIZ_POSITION;
			layer[i].transform.position = lPosition;
		}
		
		//유저위치 복구
		Vector3 uPosition = GlobalScript.userPosition;
		uPosition.x += BEFORE_QUIZ_POSITION;
		user.transform.position = uPosition;

	}
}
