using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TutorialFieldManager : MonoBehaviour {

	public List<GameObject> layer=new List<GameObject>();
	public List<float> layer_speed =new List<float>();
	public GameObject user;
	public UIManager uiManager;
	public SoundManager soundManager;
	public GameObject star;
	public FadeEffect fadeEffect;
	
	private const float BEFORE_QUIZ_POSITION = 8f; //퀴즈 후에 앞으로 이동할 거리
	
	float userSpeed;
	private bool stopMoving = false;

	//배경스크롤
	void Update () {
		if (!stopMoving) {
			for (int i = 0; i < layer.Count; i++) {
				layer[i].transform.Translate(Vector3.right * (userSpeed-layer_speed[i]) * Time.deltaTime);
			}
		}
		
		if (user.GetComponent<User>().catCollision) { //야옹충돌 이벤트 이후
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit) {
				if (hit.collider.gameObject.name.Equals("catArea_init")) quizStart("Quiz_initial");
				else if (hit.collider.gameObject.name.Equals("catArea_choice")) quizStart("Quiz_choice");
			}
		}
		
	}
	
	
	void Start () {
		
		//테스트용, 반드시 지울것
//		GlobalScript.setLife(3);
		
		//페이드인 효과
		fadeEffect.FadeIn(1f);
		
		userSpeed = user.GetComponent<User>().userSpeed; //속도
		
		//점수표시
		uiManager.GetComponent<UIManager>().setScoreText();
		
		//목숨표시(Life만큼의 칸을 표시해줌)
		uiManager.GetComponent<UIManager>().updateLifeBar();
		
		//음악퀴즈를 풀고 필드로 복귀
		if (!GlobalScript.userPosition.Equals(new Vector3(0, 0, 0))) {			
			returnFromMusicQuiz();
		}
	}
	
	
	//음악퀴즈에서 필드로 돌아왔을때의 처리
	void returnFromMusicQuiz() {
		
		//레이어들의 위치를 퀴즈풀기 이전으로 복구
		loadPosition();
		
		//정답표시
		uiManager.GetComponent<UIManager>().showAnswer();
		
		//정답
		if (GlobalScript.lifeEvent == 0) {
			user.GetComponent<User>().startShowIcon(2); //정답아이콘
			GlobalScript.modifyScore(100); //점수
			uiManager.raiseScore(100); //캐릭터 위에 오버랩되는 효과
			soundManager.correctPlay();
		}
		
		//오답
		else if (GlobalScript.lifeEvent == -1) {
			user.GetComponent<User>().startShowIcon(3); //오답아이콘
			uiManager.GetComponent<UIManager>().decreaseLifeBar();
			GlobalScript.modifyLife(-1); //이건 반드시 decreaseLifeBar뒤에 와야한다
			
			//사망여부확인
			if (GlobalScript.getLife() <= 0) {
				savePosition(); //부활을 대비해서 현제상태 기억
				StartCoroutine(userDie(1));
			}
		}
	}


	
	//[게임오버] 메인 코루틴
	IEnumerator userDie(float delayTime) {

		//UI게임오버효과
		uiManager.GetComponent<UIManager>().UIUserDie(); 
		yield return new WaitForSeconds(delayTime);
		
		//기력딸려서 속도느려짐
		user.GetComponent<User>().userSpeed = 0.1f;
		
		//죽음을 직감한 멘트
		uiManager.GetComponent<UIManager>().showText_Long("텐션이 떨어진다...");
	}

 
	//일시정지
	public void pauseMove() {
		stopMoving = true;
		user.GetComponent<User>().userSpeed = 0; //정지
	}

	//다시움직
	public void resumeMove() {
		stopMoving = false;
		user.GetComponent<User>().userSpeed = userSpeed;
	}


	//점프할때 배경의 변화
	public void bgJumpEffect() {
		star.GetComponent<Animator>().SetTrigger("jump_t");
	}


	//퀴즈씬으로 이동
	public void quizStart(String quizType) {
		savePosition(); //현재 레이어(유저포함)들의 위치를 전역에 기억
		SceneManager.LoadScene(quizType);		
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
	
	
	//카메라 시점변화(사용X)
	IEnumerator rotateCamera(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
//	
//		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
//		cam.transform.Rotate(Vector3.up * 0.1f); 
//		cam.transform.Translate( Vector3.back * 0.005f);
//		StartCoroutine("rotateCamera", 0.1f);
	}


}
