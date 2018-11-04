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

	void Start() {
		connectServer.quiz_2(0);
		sightMove();
	}

	void sightMove() {
		sight.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 600);
		StartCoroutine(makeBar(2));
	}

	IEnumerator makeBar(float delayTime) {
		yield return new WaitForSeconds(delayTime);

		bar.SetActive(true);
	}

	
	//ConnectServer에서 이곳을 호출하면서 퀴즈 시작
	public void setGame(Quiz quiz) {

		GlobalScript.answerStr = "곡명\n" + quiz.singer + " - " + quiz.title; //정답표시 설정	


		Debug.Log(quiz.choices[0].choice);
		Debug.Log(quiz.choices[1].choice);
		Debug.Log(quiz.choices[2].choice);
		
	}


}
