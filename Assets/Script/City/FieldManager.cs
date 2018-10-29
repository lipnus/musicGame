using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FieldManager : MonoBehaviour {

	public GameObject sky;
	public GameObject cloud;
	public GameObject building;
	public GameObject user;
	public GameObject near;

	float userSpeed;

	void Update () {
		sky.transform.Translate(Vector3.right * userSpeed * Time.deltaTime);
		cloud.transform.Translate(Vector3.right * (userSpeed-0.1f) * Time.deltaTime);
		building.transform.Translate(Vector3.right * (userSpeed-1.3f) * Time.deltaTime);
	}
	
	
	void Start () {
        userSpeed = 2.5f;
		user.transform.position = GlobalScript.userPosition;
		sky.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		sky.transform.Translate(Vector3.forward*10f);
		
		
		//점수표시
		GlobalScript.showScoreText();
		
		//목숨표시(Life만큼의 칸을 표시해줌)
		GameObject.Find("UIManager").GetComponent<UIManager>().updateLifeBar();

		//음악퀴즈를 풀고 돌아온 경우
		if (!GlobalScript.userPosition.Equals(new Vector3(0, 0, 0))) {
			returnFromMusicQuiz();
		}
	}
	
	
	//음악퀴즈에서 필드로 돌아왔을때의 처리
	void returnFromMusicQuiz() {
		string answerStr = GlobalScript.answerStr;
		GameObject.Find("UIManager").GetComponent<UIManager>().showText_Long(answerStr);
		
		//목숨처리
		if (GlobalScript.lifeEvent == 0) {//정답
			
		}else if (GlobalScript.lifeEvent == -1) {//오답
			GameObject.Find("UIManager").GetComponent<UIManager>().decreaseLifeBar();
			GlobalScript.modifyLife(-1); //이건 반드시 decreaseLifeBar뒤에 와야한다
			
			//사망여부확인
			if (GlobalScript.getLife() <= 0) {
				Debug.Log("다이");
			}
		}
	}

	
    public void userSpeedControl(float percent){
        this.userSpeed *= percent;
    }

	//고양이 만났을 때
	public void catEffect() {
	
		//투시시점
		Camera.main.orthographic = false;
		GameObject.Find("Sky").transform.localScale += new Vector3(2f, 2f, 0);
	
		//카메라초점거리
		GameObject.Find("Main Camera").GetComponent<Camera>().focalLength = 4f;
		
		StartCoroutine("rotateCamera", 0.1);
	}
	
	
	//카메라 시점변화
	IEnumerator rotateCamera(float delayTime) { 
		yield return new WaitForSeconds(delayTime);
	
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		cam.transform.Rotate(Vector3.up * 0.15f); 
		cam.transform.Translate( Vector3.back * 0.005f);
	
		StartCoroutine("rotateCamera", 0.05f);
	}
}
