using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StageFieldManager : MonoBehaviour {

	public List<GameObject> layer = new List<GameObject>();
	public List<float> layer_speed = new List<float>();
	public GameObject user;
	public GameObject star;
	
	public UIManager uiManager;
	public SoundManager soundManager;
	public FadeEffect fadeEffect;
	public GameObject bossCat;
	private const float BEFORE_QUIZ_POSITION = 10f; //퀴즈 후에 돌아와서 더 앞으로 이동할 거리
	
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
			if (hit && hit.collider.gameObject.name.Equals("catArea")) {

				CatObject catObj = hit.collider.GetComponent<CatObject>();
				Utils.difficulty = catObj.difficulty;
				
				if ( catObj.quizType==CatObject.QuizType.Initial ) quizStart("Quiz_initial");
				else if ( catObj.quizType==CatObject.QuizType.Choice ) quizStart("Quiz_choice");
			}
		}
		
	}
	
	
	void Start () {

		//페이드인 효과
		fadeEffect.FadeIn(1f);
		
		//속도
		userSpeed = user.GetComponent<User>().userSpeed; 
		
		//닉네임표시
		uiManager.GetComponent<UIManager>().setNickname();
		
		//점수표시
		uiManager.GetComponent<UIManager>().setPointText();
		
		//목숨표시(Life만큼의 칸을 표시해줌)
		uiManager.GetComponent<UIManager>().updateLifeBar();
		
		//음악퀴즈를 풀고 필드로 복귀한 경우
		if (!Utils.userPosition.Equals(new Vector3(0, 0, 0))) {			
			returnFromMusicQuiz();
		}
		
		
		//마지막미션인 경우
		if (Application.loadedLevelName.Equals("RiverScene")) {
			setBossCat();
		}
	}
	
	
	//음악퀴즈에서 필드로 돌아왔을때의 처리
	void returnFromMusicQuiz() {
		
		//레이어들의 위치를 퀴즈풀기 이전으로 복구
		if (Utils.getPlayData().bossLife == 1 || Utils.getPlayData().bossLife == 2) {
			loadPositionInfrontOfBossCat();				
		}
		else {
			loadPosition();
		}


		//정답표시
		uiManager.GetComponent<UIManager>().showAnswer();
		
		//정답
		if (Utils.lifeEvent == 0) {
			user.GetComponent<User>().startShowIcon(1); //정답아이콘
			Utils.modifyPoint(100); //점수
			Utils.modifyCorrect(1); //맞은개수
			uiManager.raisePoint(100); //캐릭터 위에 오버랩되는 효과
			soundManager.correctPlay();
		}
		
		//오답
		else if (Utils.lifeEvent == -1) {
			user.GetComponent<User>().startShowIcon(2); //오답아이콘
			uiManager.GetComponent<UIManager>().decreaseLifeBar();
			Utils.modifyWrong(1);
			Utils.modifyLife(-1); //이건 반드시 decreaseLifeBar뒤에 와야한다
			
			//사망여부확인
			if (Utils.getLife() <= 0) {
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
		uiManager.GetComponent<UIManager>().showText("텐션이 떨어진다...");
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
		Utils.sceneName = Application.loadedLevelName; //스테이지 기억
		
		//레이어위치 저장
		Utils.positionHolder.Clear();
		for (int i = 0; i < layer.Count; i++) {
			Utils.positionHolder.Add( layer[i].transform.position );
		}
		
		//유저위치 저장
		Utils.userPosition = user.transform.position;
	}
	
	
	//모든 레이어의 위치를 전역에서 불러옴
	public void loadPosition() {
		//레이어 복구
		for (int i = 0; i < layer.Count; i++) {
			Vector3 lPosition = Utils.positionHolder[i];

			lPosition.x += BEFORE_QUIZ_POSITION; //위치보정				
			layer[i].transform.position = lPosition;
		}
		
		//유저위치 복구
		Vector3 uPosition = Utils.userPosition;
		
		uPosition.x += BEFORE_QUIZ_POSITION;
		user.transform.position = uPosition;
	}

	
	
	//대왕고양이 앞에서는 앞으로 넘어가지 않고 오히려 뒤로 넘어감
	public void loadPositionInfrontOfBossCat() {
		
		Debug.Log("보스목숨:" + Utils.getPlayData().bossLife);
		
		//레이어 복구
		for (int i = 0; i < layer.Count; i++) {
			Vector3 lPosition = Utils.positionHolder[i];

			lPosition.x -= BEFORE_QUIZ_POSITION; //위치보정				
			layer[i].transform.position = lPosition;
		}
		
		//유저위치 복구
		Vector3 uPosition = Utils.userPosition;
		
		uPosition.x -= BEFORE_QUIZ_POSITION;
		user.transform.position = uPosition;
	}



	public void setBossCat() {		
		
		if (Utils.getPlayData().bossLife == 2) {
			float catScale = 3.3f;
			bossCat.transform.localScale = new Vector3(catScale, catScale, 1f);
			bossCat.transform.Find("catArea").gameObject.GetComponent<CatObject>().quizType =
				CatObject.QuizType.Initial;

		}

		if (Utils.getPlayData().bossLife == 1) {
			float catScale = 0.8f;
			bossCat.transform.localScale = new Vector3(catScale, catScale, 1f);
			bossCat.GetComponent<BoxCollider2D>().size = new Vector2(30f, 5f);
			bossCat.GetComponent<CatObject>().quizType = CatObject.QuizType.Initial;
		}

		if (Utils.getPlayData().bossLife == 0) {
			bossCat.active = false;
		}
		
				
	}
	
	

}
