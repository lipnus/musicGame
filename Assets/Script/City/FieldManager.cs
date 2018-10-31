using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class FieldManager : MonoBehaviour {

	public GameObject layer_1;
	public float layer1_speed;
	public GameObject layer_2;
	public float layer2_speed;
	public GameObject layer_3;
	public float layer3_speed;
	public GameObject user;
	public GameObject near;



 
	float userSpeed;

	void Update () { 
		layer_1.transform.Translate(Vector3.right * (userSpeed-layer1_speed) * Time.deltaTime);
		layer_2.transform.Translate(Vector3.right * (userSpeed-layer2_speed) * Time.deltaTime);
		layer_3.transform.Translate(Vector3.right * (userSpeed-layer3_speed) * Time.deltaTime);	
	}
	
	
	void Start () {
		
		//테스트용, 반드시 지울것
		GlobalScript.setLife(1);
		
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
		
		user.transform.position = GlobalScript.userPosition;
			
		//배경위치 보정
		layer_1.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		layer_1.transform.Translate(Vector3.forward*10f);
		
		
		
		GameObject.Find("UIManager").GetComponent<UIManager>().showText_Long(answerStr);
		
		//목숨처리
		if (GlobalScript.lifeEvent == 0) {//정답
			
		}else if (GlobalScript.lifeEvent == -1) {//오답
			GameObject.Find("UIManager").GetComponent<UIManager>().decreaseLifeBar();
			GlobalScript.modifyLife(-1); //이건 반드시 decreaseLifeBar뒤에 와야한다
			
			//사망여부확인
			if (GlobalScript.getLife() <= 0) {
				StartCoroutine(userDie(1));
			}
		}
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

	
	
    public void userSpeedControl(float percent){
        this.userSpeed *= percent;
    }

	//고양이 만났을 때
	public void catEffect() {
	
		//투시시점
		Camera.main.orthographic = false;
//		GameObject.Find("Sky").transform.localScale += new Vector3(2f, 2f, 0);
	
		//카메라초점거리
		GameObject.Find("Main Camera").GetComponent<Camera>().focalLength = 4f;
		
		StartCoroutine("rotateCamera", 0.1);
	}
	
	
	//카메라 시점변화
	IEnumerator rotateCamera(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
	
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		cam.transform.Rotate(Vector3.up * 0.1f); 
		cam.transform.Translate( Vector3.back * 0.005f);
	
		StartCoroutine("rotateCamera", 0.1f);
	}
}
