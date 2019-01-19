using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Subway : MonoBehaviour {
	

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject.Find("subway_icon").GetComponent<Animator>().SetBool("subway_b", true); //아이콘 표시
		GameObject.Find("fadeEffect").GetComponent<FadeEffect>().FadeOut(3f); //페이드아웃
		StartCoroutine(moveNextStage(5f));//2초후 이동
	}

	IEnumerator moveNextStage(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		
		//스테이지 관련 값 초기화
		GlobalScript.resetStage();
		if(Application.loadedLevelName.Equals("CityScene")) SceneManager.LoadScene("RiverScene");

	}
}
